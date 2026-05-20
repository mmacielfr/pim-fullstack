"""
Etapa 7 — Análise de dados do aluguel de motos KTM.
Lê o CSV fictício, gera indicadores, gráficos e uma predição simples.
"""

from pathlib import Path

import matplotlib.pyplot as plt
import pandas as pd
from sklearn.linear_model import LinearRegression

BASE = Path(__file__).parent
CSV = BASE / "dados" / "alugueis_historico.csv"
RELATORIOS = BASE / "relatorios"


def carregar() -> pd.DataFrame:
    df = pd.read_csv(CSV, parse_dates=["data_aluguel"])
    df["mes_ano"] = df["data_aluguel"].dt.to_period("M").astype(str)
    return df


def indicadores(df: pd.DataFrame) -> None:
    confirmados = df[df["confirmado"] == "sim"]

    print("\n" + "=" * 50)
    print("  RELATÓRIO — Aluguel de Motos KTM")
    print("=" * 50)

    print(f"\nTotal de solicitações: {len(df)}")
    print(f"Aluguéis confirmados:  {len(confirmados)}")
    print(f"Taxa de confirmação:   {len(confirmados) / len(df) * 100:.1f}%")

    print("\n--- Aluguéis por categoria (confirmados) ---")
    por_cat = confirmados.groupby("categoria").size().sort_values(ascending=False)
    for cat, qtd in por_cat.items():
        pct = qtd / len(confirmados) * 100
        print(f"  {cat}: {qtd} ({pct:.1f}%)")

    print("\n--- Receita mensal estimada (confirmados) ---")
    receita_cat = confirmados.groupby("categoria")["valor_mensal"].sum().sort_values(ascending=False)
    for cat, valor in receita_cat.items():
        print(f"  {cat}: R$ {valor:,.0f}")

    ticket = confirmados.groupby("categoria")["valor_mensal"].mean()
    print("\n--- Ticket médio por categoria ---")
    for cat, valor in ticket.items():
        print(f"  {cat}: R$ {valor:,.0f}/mês")

    print("\n--- Modelo mais alugado ---")
    top = confirmados["modelo"].value_counts().head(3)
    for modelo, qtd in top.items():
        print(f"  {modelo}: {qtd} aluguéis")


def graficos(df: pd.DataFrame) -> None:
    confirmados = df[df["confirmado"] == "sim"]
    RELATORIOS.mkdir(exist_ok=True)

    fig, axes = plt.subplots(1, 3, figsize=(15, 4))

    por_cat = confirmados.groupby("categoria").size()
    axes[0].bar(por_cat.index, por_cat.values, color=["#ff6600", "#333333", "#cc0000"])
    axes[0].set_title("Aluguéis por categoria")
    axes[0].set_ylabel("Quantidade")

    receita = confirmados.groupby("categoria")["valor_mensal"].sum()
    axes[1].pie(receita.values, labels=receita.index, autopct="%1.1f%%", startangle=90)
    axes[1].set_title("Participação na receita")

    por_mes = confirmados.groupby("mes_ano").size()
    axes[2].plot(por_mes.index, por_mes.values, marker="o", color="#ff6600")
    axes[2].set_title("Aluguéis confirmados por mês")
    axes[2].tick_params(axis="x", rotation=45)

    plt.tight_layout()
    caminho = RELATORIOS / "dashboard_alugueis.png"
    plt.savefig(caminho, dpi=120)
    plt.close()
    print(f"\nGráfico salvo em: {caminho}")


def prever_proximo_mes(df: pd.DataFrame) -> None:
    confirmados = df[df["confirmado"] == "sim"].copy()
    por_mes = confirmados.groupby("mes_ano").size().reset_index(name="alugueis")
    por_mes["indice"] = range(len(por_mes))

    if len(por_mes) < 3:
        print("\nDados insuficientes para predição.")
        return

    X = por_mes[["indice"]].values
    y = por_mes["alugueis"].values

    modelo = LinearRegression()
    modelo.fit(X, y)
    proximo = int(round(modelo.predict([[len(por_mes)]])[0]))
    proximo = max(proximo, 0)

    print("\n--- Predição (regressão linear) ---")
    print(f"Aluguéis previstos no próximo mês: ~{proximo}")

    ticket_medio = confirmados["valor_mensal"].mean()
    receita_prevista = proximo * ticket_medio
    print(f"Receita mensal estimada: R$ {receita_prevista:,.0f} (ticket médio R$ {ticket_medio:,.0f})")


def interpretacao(df: pd.DataFrame) -> None:
    confirmados = df[df["confirmado"] == "sim"]
    cat_top = confirmados["categoria"].value_counts().idxmax()
    modelo_top = confirmados["modelo"].value_counts().idxmax()

    print("\n" + "=" * 50)
    print("  INTERPRETAÇÃO — Apoio à decisão")
    print("=" * 50)
    print(f"""
• A categoria "{cat_top}" lidera em volume — faz sentido manter estoque
  e visibilidade no site (página de modelos).

• "{modelo_top}" é o modelo mais procurado — priorizar manutenção
  e disponibilidade dessa unidade.

• Off-Road tem ticket mais alto (R$ 2.199/mês) — mesmo com menos
  volume, contribui bem na receita; vale campanhas sazonais.

• Taxa de cancelamento ~{len(df[df['confirmado'] == 'nao']) / len(df) * 100:.0f}% — revisar
  etapas do formulário no backend para reduzir desistências.

• Na integração futura, cada aluguel confirmado no console C# alimentará
  este CSV e as predições passarão a refletir dados reais.
""")


def main() -> None:
    if not CSV.exists():
        raise FileNotFoundError(f"CSV não encontrado: {CSV}\nRode: python gerar_dados.py")

    df = carregar()
    indicadores(df)
    graficos(df)
    prever_proximo_mes(df)
    interpretacao(df)
    print("\nAnálise concluída.\n")


if __name__ == "__main__":
    main()
