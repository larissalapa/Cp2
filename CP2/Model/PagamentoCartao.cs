namespace SistemaPagamentos.model
{
    public class PagamentoCartao : Pagamento
    {
        private string numeroCartao;

        public string NumeroCartao
        {
            get { return numeroCartao; }
            set
            {
                // Remove tudo que não for dígito
                string digits = "";
                foreach (char c in value)
                {
                    if (char.IsDigit(c))
                        digits += c;
                }

                if (digits.Length != 16)
                    throw new ArgumentException("Número do cartão inválido! Deve conter exatamente 16 dígitos numéricos.");

                // Formata: 0000-0000-0000-0000
                numeroCartao = string.Format("{0}-{1}-{2}-{3}",
                    digits.Substring(0, 4),
                    digits.Substring(4, 4),
                    digits.Substring(8, 4),
                    digits.Substring(12, 4));
            }
        }

        public void Registrar(decimal valor, string numCartao)
        {
            base.Registrar(valor);
            NumeroCartao = numCartao;
        }

        public override string ProcessarPagamento()
        {
            string valorFormatado = Valor.ToString("N2", new System.Globalization.CultureInfo("pt-BR"));
            return $"Processando pagamento de R$ {valorFormatado} via Cartão " +
                    $"(Número: {NumeroCartao}) na data {DataPagamento:dd/MM/yyyy}.";
        }
    }
}