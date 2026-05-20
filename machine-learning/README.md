# Etapa 7 — Machine Learning e Análise de Dados

Análise de dados do **aluguel de motos KTM**, módulo independente (sem integração com front/backend por enquanto).

## Tipos de dados relevantes

| Coluna | Descrição | Uso na análise |
|--------|-----------|----------------|
| `data_aluguel` | Data da solicitação | Sazonalidade e tendência |
| `mes` | Mês (1–12) | Agrupamento temporal |
| `dia_semana` | Dia da semana | Padrões de demanda |
| `categoria` | Off-Road, Naked, Sport | Segmentação do negócio |
| `modelo` | Moto KTM específica | Modelo mais procurado |
| `valor_mensal` | Preço do aluguel (R$) | Receita e ticket médio |
| `cilindrada` | Cilindrada (cm³) | Perfil técnico |
| `confirmado` | sim / nao | Taxa de conversão |

Os valores seguem o catálogo do front (Duke 390, RC 390, 525 EXC) e o backend C#.

## Técnicas utilizadas

- **Análise descritiva** com `pandas` (contagens, médias, agrupamentos)
- **Visualização** com `matplotlib` (barras, pizza, linha temporal)
- **Machine learning básico** com `scikit-learn` (regressão linear para prever aluguéis no próximo mês)

## Como rodar

```bash
cd machine-learning

# 1. Criar ambiente virtual (recomendado)
python3 -m venv .venv
source .venv/bin/activate   # Linux/macOS
# .venv\Scripts\activate    # Windows

# 2. Instalar dependências
pip install -r requirements.txt

# 3. Gerar o CSV fictício (se ainda não existir)
python gerar_dados.py

# 4. Executar a análise
python analise.py
```

A saída inclui indicadores no terminal, gráfico em `relatorios/dashboard_alugueis.png` e predição do próximo mês.

## Indicadores gerados

- Total de solicitações e aluguéis confirmados
- Taxa de confirmação
- Aluguéis por categoria
- Receita e ticket médio por categoria
- Modelo mais alugado
- Predição de aluguéis e receita no próximo mês

## Interpretação no contexto do negócio

A locadora KTM fictícia pode usar os resultados para:

1. **Estoque** — reforçar a categoria com maior demanda
2. **Precificação** — Off-Road tem ticket alto; Naked tem volume
3. **Marketing** — focar no modelo mais procurado
4. **Operação** — reduzir cancelamentos melhorando o fluxo do formulário

## Integração futura (não implementada)

Quando o backend C# confirmar um aluguel, uma linha será gravada em `dados/alugueis_historico.csv` com as mesmas colunas. O script `analise.py` não precisará mudar — apenas passará a ler dados reais.

## Estrutura

```
machine-learning/
├── dados/
│   └── alugueis_historico.csv
├── relatorios/          # gerado ao rodar analise.py
├── gerar_dados.py
├── analise.py
├── requirements.txt
└── README.md
```
