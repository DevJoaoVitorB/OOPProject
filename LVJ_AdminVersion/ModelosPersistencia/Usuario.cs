class Usuario
{
    public int id { get; set; }
    public string nome { get; set; }
    public string email { get; set; }
    public string senha { get; set; }
    public string endereco { get; set; }
    public string CEP { get; set; }
    public string CPF { get; set; }
    public bool admin { get; set; }

    public Usuario(int id, string nome, string email, string senha, string endereco, string CEP, string CPF, bool admin)
    {
        this.id = id;
        this.nome = nome;
        this.email = email;
        this.senha = senha;
        this.endereco = endereco;
        this.CEP = CEP;
        this.CPF = CPF;
        this.admin = admin;
    }

    public override string ToString()
    {
        if(admin == true){
            return $"{id} - {nome} \n\tEmail: {email} \n\tSenha: {senha} \n";
        } else {
            return $"{id} - {nome} \n\tEmail: {email} \n\tSenha: {senha} \n\tEndereço: {endereco} \n\tCEP: {CEP} \n\tCPF: {CPF} \n";
        }
    }
}