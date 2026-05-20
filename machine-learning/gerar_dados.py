"""Gera dataset fictício de aluguéis KTM (mesmo formato previsto para integração futura com o backend)."""

import csv
import random
from datetime import date, timedelta
from pathlib import Path

MOTOS = [
    ("Off-Road", "KTM 525 EXC 2006", 2199, 510),
    ("Naked", "KTM Duke 390", 1499, 373),
    ("Sport", "KTM RC 390", 1799, 373),
]

PESO_CATEGORIA = [0.28, 0.42, 0.30]
DIAS_SEMANA = ["segunda", "terca", "quarta", "quinta", "sexta", "sabado", "domingo"]

SAIDA = Path(__file__).parent / "dados" / "alugueis_historico.csv"


def gerar(registros: int = 120, seed: int = 42) -> None:
    random.seed(seed)
    inicio = date(2025, 1, 1)
    fim = date(2026, 4, 30)

    linhas = []
    for i in range(1, registros + 1):
        categoria, modelo, valor, cilindrada = random.choices(MOTOS, weights=PESO_CATEGORIA, k=1)[0]
        dias = random.randint(0, (fim - inicio).days)
        data = inicio + timedelta(days=dias)
        confirmado = random.random() > 0.14

        linhas.append({
            "id": i,
            "data_aluguel": data.isoformat(),
            "mes": data.month,
            "dia_semana": DIAS_SEMANA[data.weekday()],
            "categoria": categoria,
            "modelo": modelo,
            "valor_mensal": valor,
            "cilindrada": cilindrada,
            "confirmado": "sim" if confirmado else "nao",
        })

    linhas.sort(key=lambda r: r["data_aluguel"])

    SAIDA.parent.mkdir(parents=True, exist_ok=True)
    with SAIDA.open("w", newline="", encoding="utf-8") as f:
        writer = csv.DictWriter(f, fieldnames=linhas[0].keys())
        writer.writeheader()
        writer.writerows(linhas)

    print(f"Gerado: {SAIDA} ({len(linhas)} registros)")


if __name__ == "__main__":
    gerar()
