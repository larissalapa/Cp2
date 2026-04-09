namespace SistemaPagamentos.model
{
    public abstract class Pagamento
    {
        private decimal valor;
        private DateOnly dataPagamento;

        public decimal Valor
        {
            get { return valor; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("O valor do pagamento deve ser maior que zero.");
                valor = value;
            }
        }

        public DateOnly DataPagamento
        {
            get { return dataPagamento; }
            set { dataPagamento = value; }
        }

        public void Registrar(decimal valor)
        {
            Valor = valor;
            DataPagamento = DateOnly.FromDateTime(DateTime.Today);
        }

        public abstract string ProcessarPagamento();
    }
}
