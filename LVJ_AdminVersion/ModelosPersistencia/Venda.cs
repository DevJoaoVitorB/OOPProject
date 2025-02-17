using System.Globalization;

class Venda
{
    public int id { get; set; }
    public DateTime dia { get; set; }
    public bool carrinho { get; set; }
    public double frete { get; set; }
    public double total { get; set; }
    public double parcela { get; set; }
    public DateTime vencimento { get; set; }
    public int idCliente { get; set; }
    public int idFormaPagamento { get; set; }

    public Venda(int id, DateTime dia, bool carrinho, double frete, double total, double parcela, DateTime vencimento, int idCliente, int idFormaPagamento)
    {
        this.id = id;
        this.dia = dia;
        this.carrinho = carrinho;
        this.frete = frete;
        this.total = total;
        this.parcela = parcela;
        this.vencimento = vencimento;
        this.idCliente = idCliente;
        this.idFormaPagamento = idFormaPagamento;
    }

    public override string ToString()
    {
        if(carrinho == true)
        {
           return $"[{id}] \n\tStatus: Aberta \n\tFrete: {frete.ToString("C", new CultureInfo("pt-BR"))} \n\tTotal a Pagar: {total.ToString("C", new CultureInfo("pt-BR"))} \n\tParcela: {parcela.ToString("C", new CultureInfo("pt-BR"))} \n";   
        } else {
            return $"[{id}] \n\tDia da Compra: {dia.AddHours(-3).ToString("dd/MM/yyyy HH:mm", new CultureInfo("pt-BR"))} \n\tStatus: Fechada \n\tFrete: R${frete:f2} \n\tTotal a Pagar: {total.ToString("C", new CultureInfo("pt-BR"))} \n\tParcela: {parcela.ToString("C", new CultureInfo("pt-BR"))} \n\tData de Vencimento: {vencimento.AddHours(-3).ToString("dd/MM/yyyy HH:mm", new CultureInfo("pt-BR"))} \n";   
        }
    }
}