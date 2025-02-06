class FormaPagamento
{
    public int id { get; set; }
    public string descricao { get; set; }
    public int parcelas { get; set; }
    public double percentual { get; set; }
    public int diaVencimento { get; set; }

    public FormaPagamento(int id, string descricao, int parcelas, double percentual, int diaVencimento)
    {
        this.id = id;
        this.descricao = descricao;
        this.parcelas = parcelas;
        this.percentual = percentual;
        this.diaVencimento = diaVencimento;
    }

    public override string ToString()
    {
        if(percentual < 0){
            return $"{id} - {descricao} \n\tQuantidade de Parcelas: {parcelas}x \n\t Desconto: {percentual * -1}% \n\tDias para Pagar: {diaVencimento} dia(s) \n";
        } else {
            return $"{id} - {descricao} \n\tQuantidade de Parcelas: {parcelas}x \n\t Juros: {percentual}% \n\tDias para Pagar: {diaVencimento} dia(s) \n";
        }
    }
}