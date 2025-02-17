using System.Text.RegularExpressions;
using System.Globalization;

static class View
{
    private static Usuarios usuarios = new Usuarios();
    private static Categorias categorias = new Categorias();
    private static Produtos produtos = new Produtos();
    private static ProdutosVendas produtosVendas = new ProdutosVendas();
    private static Vendas vendas = new Vendas();
    private static FormasPagamento formasPagemento = new FormasPagamento();

    public static void CriarAdmin()
    {
        if(!ListarUsuarios().Exists(x => x.admin)) InserirUsuarios("João Vitor Bezerra", "admin@email.com", "admin1234", "", "", "", true);
    }

    public static bool ValidarLogin(string email, string senha, out int idLogado)
    {
        foreach(Usuario x in ListarUsuarios()) if(x.email == email && x.senha == senha) {idLogado = x.id; return true;}
        idLogado = 0;
        return false;
    }

    public static void ValidarDadosUsuarios(string nome, string email, string senha, string endereco, string cep, string cpf, bool admin, out string nomeFormatado, string emailAlterado = "", string cpfAlterado = "")
    {
        // Lista de Erros de Entradas de Dados
        List<string> erros = new List<string>();
        nomeFormatado = "";
        // Formatação de E-mail
        Regex regex = new Regex(@"^[a-zA-Z0-9_+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$");

        // Verificação e Formatação de Nome dos Usuários
        if(string.IsNullOrWhiteSpace(nome)) erros.Add("\tO campo Nome deve ser preenchido!");
        else
        {
            string[] palavrasNaoFormatadas = new string[] {"de", "da", "do", "das", "dos", "e", "a", "as", "os"};
            TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;

            nome = textInfo.ToTitleCase(nome.ToLower());
            List<string> nomeFragmentado = nome.Split(" ").Select(x => palavrasNaoFormatadas.Contains(x.ToLower()) ? x.ToLower() : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.ToLower())).ToList();

            nomeFormatado = string.Join(" ", nomeFragmentado);
        }
        // Verificação de E-mail dos Usuários
        if(string.IsNullOrWhiteSpace(email)) erros.Add("\tO campo E-mail deve ser preenchido!");
        else
        {
            if(!regex.IsMatch(email)) erros.Add("\tO E-mail informado é inválido!");
            if(!string.IsNullOrWhiteSpace(emailAlterado) && ListarUsuarios().Exists(x => x.email == email)) erros.Add("\tO E-mail informado já está cadastrado!");
        }
        // Verificação de Senha dos Usuários
        if(string.IsNullOrWhiteSpace(senha)) erros.Add("\tO campo Senha deve ser preenchido!");
        else
        {
            if(senha.Length < 8) erros.Add("\tA Senha deve conter no mínimo 8 caracteres!");
        }

        // Validações de Dados de Clientes
        if(!admin)
        {
            // Verificação de Endereço de Clientes
            if(string.IsNullOrWhiteSpace(endereco)) erros.Add("\tO campo Endereço deve ser preenchido!");
            // Verificação de CEP de Clientes
            if(string.IsNullOrWhiteSpace(cep)) erros.Add("\tO campo CEP deve ser preenchido!");
            else
            {
                if(cep.Length != 8) erros.Add("\tO CEP deve conter apenas 8 caracteres númericos!");
                if(!cep.All(char.IsDigit)) erros.Add("\tO CEP deve conter apenas digítos númericos");
                if(!string.IsNullOrWhiteSpace(cpfAlterado) && ListarUsuarios().Exists(x => x.cpf == cpf)) erros.Add("\tO CPF informado já está cadastrado!");
            }
            // Verificação de CPF de Clientes
            if(string.IsNullOrWhiteSpace(cpf)) erros.Add("\tO campo CPF deve ser preenchido!");
            else
            {
                if(cpf.Length != 11) erros.Add("\tO CPF deve conter apenas 11 caracteres númericos!");
                if(!cpf.All(char.IsDigit)) erros.Add("\tO CPF deve conter apenas digítos númericos");
            }
        }

        if(erros.Count > 0) throw new ArgumentException($"----- Cadastro Não Realizado! Erros Encontrados ----- \n{string.Join(Environment.NewLine, erros)} \n");
    }

    public static void InserirUsuarios(string nome, string email, string senha, string endereco, string cep, string cpf, bool admin)
    {
        // Validação de Dados de Usuário
        ValidarDadosUsuarios(nome, email, senha, endereco, cep, cpf, admin, out string nomeFormatado);
        // Inserir Cadastro de Usuário
        Usuario x = new Usuario(0, nomeFormatado, email, senha, endereco, cep, cpf, admin);
        usuarios.Inserir(x);
    }

    public static void RemoverUsuarios(int id)
    {
        // Usuario cujo Cadastro será removido
        Usuario x = usuarios.ListarId(id);
        // Remover Cadastro de Usuário
        usuarios.Remover(x);
    }

    public static void AtualizarUsuarios(int id, string nome, string email, string senha, string endereco, string cep, string cpf, bool admin)
    {
        // Usuario cujo Cadastro será atualizado
        Usuario x = usuarios.ListarId(id);

        // Verificações de Atualização de Dados do Cadastro do Usuário
        if(!string.IsNullOrEmpty(nome) || !string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(senha) || !string.IsNullOrEmpty(endereco) || !string.IsNullOrEmpty(cep) || !string.IsNullOrEmpty(cpf))
        {
            if(!string.IsNullOrEmpty(nome)) x.nome = nome;
            if(!string.IsNullOrEmpty(email)) x.email = email;
            if(!string.IsNullOrEmpty(senha)) x.senha = senha;
            
            if(!admin)
            {
                if(!string.IsNullOrEmpty(endereco)) x.endereco = endereco;
                if(!string.IsNullOrEmpty(cep)) x.cep = cep;
                if(!string.IsNullOrEmpty(cpf)) x.cpf = cpf;
            }
        }

        // Validação de Dados de Usuário
        ValidarDadosUsuarios(x.nome, x.email, x.senha, x.endereco, x.cep, x.cpf, x.admin, out string nomeFormatado, email, cpf);
        x.nome = nomeFormatado;

        // Atualizar Cadastro de Usuario
        usuarios.Atualizar(x);
    }

    public static List<Usuario> ListarUsuarios()
    {
        return usuarios.Listar();
    }

    public static void VerificarIdUsuarios(int id, bool admin)
    {
        // Encontrar o Usuário cujo ID de Cadastro foi Informado
        Usuario x = usuarios.ListarId(id);
        // Saber se o Cadastro é de um Cliente ou Administrador
        string tipo = admin ? "Admin" : "Cliente";
        // Verificação do ID informado
        if(x == null || x.admin != admin) throw new ArgumentException($"O Nº informado não corresponde a Nenhum {tipo}");
    }
}