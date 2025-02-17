class Usuario
{
    public int id { get; set; }
    public string nome { get; set; }
    public string email { get; set; }
    public string senha { get; set; }
    public string endereco { get; set; }
    public string cep { get; set; }
    public string cpf { get; set; }
    public bool admin { get; set; }

    public Usuario(int id, string nome, string email, string senha, string endereco, string cep, string cpf, bool admin)
    {
        this.id = id;
        this.nome = nome;
        this.email = email;
        this.senha = senha;
        this.endereco = endereco;
        this.cep = cep;
        this.cpf = cpf;
        this.admin = admin;
    }

    public override string ToString()
    {
        if(admin == true){
            return $"[{id}] - {nome} \n\tEmail: {email} \n\tSenha: {senha} \n";
        } else {
            return $"[{id}] - {nome} \n\tEmail: {email} \n\tSenha: {senha} \n\tEndereço: {endereco} \n\tCEP: {Convert.ToDouble(cep).ToString(@"00\.000-000")} \n\tCPF: {Convert.ToDouble(cpf).ToString(@"000\.000\.000-00")} \n";
        }
    }
}