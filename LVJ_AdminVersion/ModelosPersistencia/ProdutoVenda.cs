using System.Globalization;

class ProdutoVenda
{
    public int id { get; set; }
    public int quantidade { get; set; }
    public double precoProduto { get; set; }
    public double precoTotal { get; set; }
    public bool resgate { get; set; }
    public string codigo { get; set; }
    public bool enviado { get; set; }
    public bool recebido { get; set; }
    public int idProduto{ get; set; }
    public int idVenda { get; set; }

    public ProdutoVenda(int id, int quantidade, double precoProduto, double precoTotal, bool resgate, string codigo, bool enviado, bool recebido, int idProduto, int idVenda)
    {
        this.id = id;
        this.quantidade = quantidade;
        this.precoProduto = precoProduto;
        this.precoTotal = precoTotal;
        this.resgate = resgate;
        this.codigo = codigo;
        this.enviado = enviado;
        this.recebido = recebido;
        this.idProduto = idProduto;
        this.idVenda = idVenda;
    }

    public override string ToString()
    {
        return $"\n\tQuantidade: {quantidade} \n\tPreço do Produto: {precoProduto.ToString("C", new CultureInfo("pt-BR"))} \n\tPreço Total: {precoTotal.ToString("C", new CultureInfo("pt-BR"))} \n";   
    }
}