class Produto
{
    public int id { get; set; }
    public string descricao { get; set; }
    public double preco { get; set; }
    public int estoque { get; set; }
    public bool digital { get; set; }
    public int idCategoria { get; set; }

    public Produto(int id, string descricao, double preco, int estoque, bool digital, int idCategoria)
    {
        this.id = id;
        this.descricao = descricao;
        this.preco = preco;
        this.estoque = estoque;
        this.digital = digital;
        this.idCategoria = idCategoria;
    }

    public override string ToString()
    {
        if(digital == true){
            return $"{id} - {descricao} \n\tPreço: R${preco} \n\tEstoque: {estoque} \n\tVersão: Digital \n";
        } else {
            return $"{id} - {descricao} \n\tPreço: R${preco} \n\tEstoque: {estoque} \n\tVersão: Física \n";
        }
    }
}