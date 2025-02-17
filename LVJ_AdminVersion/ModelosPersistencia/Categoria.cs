class Categoria
{
    public int id { get; set; }
    public string descricao { get; set; }
    public double desconto { get; set; }

    public Categoria(int id, string descricao, double desconto)
    {
        this.id = id;
        this.descricao = descricao;
        this.desconto = desconto;
    }

    public override string ToString()
    {
        if(desconto == 0)
        {
            return $"[{id}] - {descricao} \n";
        } else {
            return $"[{id}] - {descricao} - {desconto}% de Desconto \n";
        }
    }
}