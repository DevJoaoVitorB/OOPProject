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
    private static List<string> erros = new List<string>();

    public static void CriarAdmin()
    {
        if(!ListarUsuarios().Exists(x => x.admin)) InserirUsuarios("João Vitor B.", "admin@email.com", "admin1234", "", "", "", true);
    }

    public static bool ValidarLogin(string email, string senha, out int idLogado)
    {
        foreach(Usuario x in ListarUsuarios()) if(x.email == email && x.senha == senha) {idLogado = x.id; return true;}
        idLogado = 0;
        return false;
    }

    public static void ValidarDadosUsuarios(string nome, string email, string senha, string endereco, string cep, string cpf, bool admin, out string nomeFormatado, string emailAlterado = "", string cpfAlterado = "")
    {
        // Evitar acumulo de erros anteriores
        erros.Clear();

        // Formatação de E-mail e Nome
        Regex regex = new Regex(@"^[a-zA-Z0-9_+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$");
        nomeFormatado = "";

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

        // Validações de Dados dos Clientes
        if(!admin)
        {
            // Verificação de Endereço dos Clientes
            if(string.IsNullOrWhiteSpace(endereco)) erros.Add("\tO campo Endereço deve ser preenchido!");
            // Verificação de CEP dos Clientes
            if(string.IsNullOrWhiteSpace(cep)) erros.Add("\tO campo CEP deve ser preenchido!");
            else
            {
                if(cep.Length != 8) erros.Add("\tO CEP deve conter apenas 8 caracteres númericos!");
                if(!cep.All(char.IsDigit)) erros.Add("\tO CEP deve conter apenas digítos númericos");
            }
            // Verificação de CPF dos Clientes
            if(string.IsNullOrWhiteSpace(cpf)) erros.Add("\tO campo CPF deve ser preenchido!");
            else
            {
                if(cpf.Length != 11) erros.Add("\tO CPF deve conter apenas 11 caracteres númericos!");
                if(!cpf.All(char.IsDigit)) erros.Add("\tO CPF deve conter apenas digítos númericos");
                if(!string.IsNullOrWhiteSpace(cpfAlterado) && ListarUsuarios().Exists(x => x.cpf == cpf)) erros.Add("\tO CPF informado já está cadastrado!");
            }
        }

        // Verificar se há erros
        if(erros.Count > 0) throw new ArgumentException($"----- Cadastro Não Realizado! Erros Encontrados ----- \n{string.Join(Environment.NewLine, erros)} \n");
    }

    public static void InserirUsuarios(string nome, string email, string senha, string endereco, string cep, string cpf, bool admin)
    {
        // Validação de Dados do Usuário
        ValidarDadosUsuarios(nome, email, senha, endereco, cep, cpf, admin, out string nomeFormatado);

        // Inserir o Cadastro do Usuário
        Usuario x = new Usuario(0, nomeFormatado, email, senha, endereco, cep, cpf, admin);
        usuarios.Inserir(x);
    }

    public static void RemoverUsuarios(int id)
    {
        // Usuário cujo Cadastro será removido
        Usuario x = ObterUsuarios(id);

        // Remover o Cadastro do Usuário
        usuarios.Remover(x);
    }

    public static void AtualizarUsuarios(int id, string nome, string email, string senha, string endereco, string cep, string cpf, bool admin)
    {
        // Usuário cujo Cadastro será atualizado
        Usuario x = ObterUsuarios(id);

        // Atualização de Dados do Cadastro do Usuário
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

        // Validação de Dados do Usuário
        ValidarDadosUsuarios(x.nome, x.email, x.senha, x.endereco, x.cep, x.cpf, x.admin, out string nomeFormatado, email, cpf);
        x.nome = nomeFormatado;

        // Atualizar o Cadastro do Usuário
        usuarios.Atualizar(x);
    }

    public static List<Usuario> ListarUsuarios()
    {
        return usuarios.Listar();
    }

    public static void VerificarIdUsuarios(int id, bool admin)
    {
        // Encontrar o Usuário cujo ID de Cadastro foi Informado
        Usuario x = ObterUsuarios(id);

        // Saber se o Cadastro é de um Cliente ou Administrador
        string tipo = admin ? "Administrador" : "Cliente";

        // Verificação do ID informado
        if(x == null || x.admin != admin) throw new ArgumentException($"O Nº informado não corresponde a nenhum {tipo}! Informe um valor correspondente a um {tipo} listado. \n");
    }

    public static Usuario ObterUsuarios(int id)
    {
        // Obter o Usuário cujo ID de Cadastro foi Informado
        return usuarios.ListarId(id);
    }

    public static void ValidarDadosCategorias(string descricao, double desconto)
    {
        // Evitar acumulo de erros anteriores
        erros.Clear();

        // Verificação de Descrição das Categorias
        if(string.IsNullOrWhiteSpace(descricao)) erros.Add("\tO campo Descrição deve ser preenchido!");

        // Verificação de Desconto das Categorias
        if(desconto < 0 || desconto > 75) erros.Add("\tO Desconto da Categoria deve ser entre 0% e 75%!");

        // Verificar se há erros
        if(erros.Count > 0) throw new ArgumentException($"----- Cadastro Não Realizado! Erros Encontrados ----- \n{string.Join(Environment.NewLine, erros)} \n");
    }

    public static void InserirCategorias(string descricao, double desconto)
    {
        // Validação de Dados da Categoria
        ValidarDadosCategorias(descricao, desconto);

        // Inserir o Cadastro da Categoria
        Categoria x = new Categoria(0, descricao, desconto);
        categorias.Inserir(x);
    }

    public static void RemoverCategorias(int id)
    {
        // Categoria cujo Cadastro será removido
        Categoria x = ObterCategorias(id);

        // Verificar se há Produtos Cadastrados vinculados ao Cadastro da Categoria que será removida
        if(ListarProdutos().Exists(x => x.idCategoria == id)) throw new InvalidOperationException("Há Produtos cadastrados nessa Categoria! Faça a Remoção ou a Atualização do Cadastro desses Produtos! \n");

        // Remover o Cadastro da Categoria
        categorias.Remover(x);
    }

    public static void AtualizarCategorias(int id, string descricao, double desconto)
    {
        // Categoria cujo Cadastro será atualizado
        Categoria x = ObterCategorias(id);

        // Atualização de Dados do Cadastro da Categoria
        if(!string.IsNullOrEmpty(descricao) || desconto != -1)
        {
            if(!string.IsNullOrEmpty(descricao)) x.descricao = descricao;
            if(desconto != -1) x.desconto = desconto;
        }

        // Validação de Dados da Categoria
        ValidarDadosCategorias(x.descricao, x.desconto);

        // Atualizar o Cadastro da Categoria
        categorias.Atualizar(x);
    }

    public static List<Categoria> ListarCategorias()
    {
        return categorias.Listar();
    }

    public static void VerificarIdCategorias(int id)
    {
        // Encontrar a Categoria cujo ID de Cadastro foi Informado
        if(ObterCategorias(id) == null) throw new ArgumentException($"O Nº informado não corresponde a nenhuma Categoria! Informe um valor correspondente a uma das Categorias listadas. \n");
    }

    public static Categoria ObterCategorias(int id)
    {
        // Obter a Categoria cujo ID de Cadastro foi Informado
        return categorias.ListarId(id);
    }

    public static void ValidarDadosProdutos(string descricao, double preco, int estoque)
    {
        // Evitar acumulo de erros anteriores
        erros.Clear();

        // Verificação de Descrição dos Produtos
        if(string.IsNullOrWhiteSpace(descricao)) erros.Add("\tO campo Descrição deve ser preenchido!");

        // Verificação de Preço dos Produtos
        if(preco < 0) erros.Add("\tO Preço do Produto deve ser maior que R$ 0,00!");

        // Verificação do Estoque dos Produtos
        if(estoque < 0) erros.Add("\tO Estoque do Produto não pode ser negativo!");

        // Verificar se há erros
        if(erros.Count > 0) throw new ArgumentException($"----- Cadastro Não Realizado! Erros Encontrados ----- \n{string.Join(Environment.NewLine, erros)} \n");
    }

    public static void InserirProdutos(string descricao, double preco, int estoque, bool digital, int idCategoria)
    {
        // Validação de Dados do Produto
        ValidarDadosProdutos(descricao, preco, estoque);

        // Inserir o Cadastro do Produto
        Produto x = new Produto(0, descricao, preco, estoque, digital, idCategoria);
        produtos.Inserir(x);
    }

    public static void RemoverProdutos(int id)
    {
        // Produto cujo Cadastro será removido
        Produto x = ObterProdutos(id);

        // Remover o Cadastro do Produto
        produtos.Remover(x);
    }

    public static void AtualizarProdutos(int id, string descricao, double preco, int estoque, bool digital, int idCategoria, int chave)
    {
        // Produto cujo Cadastro será atualizado
        Produto x = ObterProdutos(id);

        // Atualização de Dados do Cadastro do Produto
        if(!string.IsNullOrEmpty(descricao) || (preco != x.preco && preco != -1) || (estoque != x.preco && estoque != -1) || (digital != x.digital && chave == 1) || (idCategoria != x.idCategoria && idCategoria != -1))
        {
            if(!string.IsNullOrEmpty(descricao)) x.descricao = descricao;
            if(preco != x.preco && preco != -1) x.preco = preco;
            if(estoque != x.estoque && estoque != -1) x.estoque = estoque;
            if(digital != x.digital && chave == 1) x.digital = digital;
            if(idCategoria != x.idCategoria && idCategoria != -1) x.idCategoria = idCategoria; 
        }

        // Validação de Dados do Produto
        ValidarDadosProdutos(x.descricao, x.preco, x.estoque);

        // Atualizar o Cadastro do Produto
        produtos.Atualizar(x);
    }

    public static List<Produto> ListarProdutos()
    {
        return produtos.Listar();
    }

    public static void VerificarIdProdutos(int id)
    {
        // Encontrar o Produto cujo ID de Cadastro foi Informado
        if(ObterProdutos(id) == null) throw new ArgumentException($"O Nº informado não corresponde a nenhum Produto! Informe um valor correspondente a um dos Produtos listados. \n");
    }

    public static Produto ObterProdutos(int id)
    {
        // Obter o Produto cujo ID de Cadastro foi Informado
        return produtos.ListarId(id);
    }

    public static void ReajusteTotalPreco(double percentual)
    {   
        // Atualizar, com o Reajuste de Preço, todos os Produtos
        ListarProdutos().ForEach(x => {x.preco *= percentual; produtos.Atualizar(x);});
    }

    public static void ReajusteCategoriaPreco(double percentual, int idCategoria)
    {
        // Atualizar, com o Reajuste de Preço, todos os Produtos da Categoria
        foreach(Produto x in ListarProdutos()) if(x.idCategoria == idCategoria) {x.preco *= percentual; produtos.Atualizar(x);}
    }

    public static void ReajusteProdutoPreco(double percentual, int idProduto)
    {
        // Produto que terá o Reajuste de Preço
        Produto x = ObterProdutos(idProduto);

        // Atualizar, com o Reajuste de Preço, o Produto
        x.preco *= percentual;
        produtos.Atualizar(x);
    }
}