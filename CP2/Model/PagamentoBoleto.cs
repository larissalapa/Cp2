using System.Drawing;

namespace SistemaPagamentos.model
{
    public class PagamentoBoleto : Pagamento
    {
        private string codigoBarras;

        public string CodigoBarras
        {
            get { return codigoBarras; }
            set
            {
                string digits = "";
                foreach (char c in value)
                {
                    if (char.IsDigit(c))
                        digits += c;
                }

                // Total de 48 digitos, segundo BC Brasil
                if (digits.Length != 48)
                    throw new ArgumentException("Código de barras inválido! Deve conter exatamente 48 dígitos.");

                codigoBarras = digits;
            }
        }

        public void Registrar(decimal valor, string codBarras)
        {
            base.Registrar(valor);
            CodigoBarras = codBarras;
        }

        public override string ProcessarPagamento()
        {
            string valorFormatado = Valor.ToString("N2", new System.Globalization.CultureInfo("pt-BR"));
            return $"Processando pagamento de R$ {valorFormatado} via Boleto " +
                   $"(Cod Barra: {CodigoBarras}) na data {DataPagamento:dd/MM/yyyy}.";
        }
    }
}