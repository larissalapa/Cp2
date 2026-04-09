using SistemaPagamentos.model;
using SistemaPagamentos.ui;
using System.Globalization;

bool running = true;

while (running)
{
    Menu.ExibirMenu();
    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            ProcessarPagamentoCartao();
            break;

        case "2":
            ProcessarPagamentoBoleto();
            break;

        case "3":
            Console.WriteLine("Encerrando o sistema. Até logo!");
            running = false;
            break;

        default:
            Console.WriteLine("Opção inválida. Por favor, escolha 1, 2 ou 3.");
            break;
    }
}

Console.ReadKey();


// ================= FUNÇÕES =================

static void ProcessarPagamentoCartao()
{
    decimal? valor = LerValor();
    if (valor == null) return;

    Console.Write("Informe o número do cartão (16 dígitos): ");
    string numeroCartao = Console.ReadLine();

    try
    {
        PagamentoCartao pagCartao = new PagamentoCartao();
        pagCartao.Registrar(valor.Value, numeroCartao);

        Console.WriteLine();
        Console.WriteLine(pagCartao.ProcessarPagamento());
    }
    catch (ArgumentException ex)
    {
        CancelarOperacao(ex.Message);
    }
}

static void ProcessarPagamentoBoleto()
{
    decimal? valor = LerValor();
    if (valor == null) return;

    Console.Write("Informe o código de barras: ");
    string codigoBarras = Console.ReadLine();

    try
    {
        PagamentoBoleto pagBoleto = new PagamentoBoleto();
        pagBoleto.Registrar(valor.Value, codigoBarras);

        Console.WriteLine();
        Console.WriteLine(pagBoleto.ProcessarPagamento());
    }
    catch (ArgumentException ex)
    {
        CancelarOperacao(ex.Message);
    }
}

static decimal? LerValor()
{
    Console.Write("Informe o valor do pagamento: ");
    string input = Console.ReadLine()?.Replace(",", ".");

    if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
        return valor;

    Console.WriteLine("Valor inválido. Tente novamente.");
    return null;
}

static void CancelarOperacao(String message)
{
    Console.WriteLine($"Erro: {message}");
    Console.WriteLine("Operação cancelada.");
}