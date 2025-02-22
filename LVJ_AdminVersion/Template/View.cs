using System.Text.RegularExpressions;
using System.Globalization;

static class View
{
    private static Usuarios usuarios = new Usuarios();
    private static Categorias categorias = new Categorias();
    private static Produtos produtos = new Produtos();
    public static ProdutosVendas produtosVendas = new ProdutosVendas();
    public static Vendas vendas = new Vendas();
    private static FormasPagamento formasPagamento = new FormasPagamento();
    private static List<string> erros = new List<string>();

    public static void CriarAdmin()
    {
        if(!ListarUsuarios().Exists(x => x.admin)) InserirUsuarios("João Vitor B.", "admin@email.com", "admin1234", "", "", "", true);
    }

    public static bool ValidarLogin(string email, string senha, out int idLogado)
    {
        Usuario logado = ListarUsuarios().SingleOrDefault(x => x.email == email && x.senha == senha && x.admin);
        if(logado != null) {idLogado = logado.id; return true;}
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
            string[] palavrasNaoFormatadas = {"de", "da", "do", "das", "dos", "e", "a", "as", "os"};
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
        if(erros.Count > 0) throw new ArgumentException($"\n----- Cadastro Não Realizado! Erros Encontrados ----- \n{string.Join(Environment.NewLine, erros)} \n");
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
        if(erros.Count > 0) throw new ArgumentException($"\n----- Cadastro Não Realizado! Erros Encontrados ----- \n{string.Join(Environment.NewLine, erros)} \n");
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
        if(ListarProdutos().Exists(x => x.idCategoria == id)) throw new InvalidOperationException("Há Produtos cadastrados nessa Categoria! Faça a Remoção ou a Atualização do Cadastro desses Produtos antes de Remover a Categoria! \n");

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
        if(preco <= 0) erros.Add("\tO Preço do Produto deve ser maior que R$ 0,00!");

        // Verificação do Estoque dos Produtos
        if(estoque < 0) erros.Add("\tO Estoque do Produto não pode ser negativo!");

        // Verificar se há erros
        if(erros.Count > 0) throw new ArgumentException($"\n----- Cadastro Não Realizado! Erros Encontrados ----- \n{string.Join(Environment.NewLine, erros)} \n");
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
        if(!string.IsNullOrEmpty(descricao) || (preco != x.preco && preco != -1) || (estoque != x.estoque && estoque != -1) || (digital != x.digital && chave == 1) || (idCategoria != x.idCategoria && idCategoria != -1))
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
        ListarProdutos().ToList().ForEach(x => {x.preco *= percentual; produtos.Atualizar(x);});
    }

    public static void ReajusteCategoriaPreco(double percentual, int idCategoria)
    {
        // Lista de Produtos para Atualizar o Preço
        List<Produto> produtosAtualizar = ListarProdutos().Where(x => x.idCategoria == idCategoria).ToList();

        // Atualizar, com o Reajuste de Preço, todos os Produtos da Categoria
        produtosAtualizar.ForEach(x => {x.preco *= percentual; produtos.Atualizar(x);});
    }

    public static void ReajusteProdutoPreco(double percentual, int idProduto)
    {
        // Produto que terá o Reajuste de Preço
        Produto x = ObterProdutos(idProduto);

        // Atualizar, com o Reajuste de Preço, o Produto
        x.preco *= percentual;
        produtos.Atualizar(x);
    }

    public static List<ProdutoVenda> ListarProdutosVendas()
    {
        return produtosVendas.Listar();
    }

    public static ProdutoVenda ObterProdutosVendas(int id)
    {
        // Obter o Produto de Venda cujo ID foi informado
        return produtosVendas.ListarId(id);
    }

    public static List<ProdutoVenda> ObterListaProdutosVendas(int idVenda)
    {
        // Obter uma Lista de Produtos de uma Venda
        return ListarProdutosVendas().Where(y => y.idVenda == idVenda).ToList();
    }

    public static void RemoverVendas(int id)
    {
        // Venda cujo Cadastro será removido
        Venda x = ObterVendas(id);

        // Verificar se a Venda foi fechada
        if(x.carrinho) throw new InvalidOperationException("Essa Venda informada não foi fechada ainda! O Cliente deve fazer a compra para habilitar a operação de remover venda!");

        // Atualizar o estoque dos Produtos que estão na Venda a ser removida
        List<ProdutoVenda> produtosVendaCliente = ObterListaProdutosVendas(id);
        produtosVendaCliente.ForEach(y => 
        {
            Produto z = ListarProdutos().SingleOrDefault(z => z.id == y.idProduto);
            z.estoque += y.quantidade; 
            produtos.Atualizar(z);
            produtosVendas.Remover(y);
        });

        // Remover o Cadastro do Produto 
        vendas.Remover(x);
    }

    public static List<Venda> ListarVendas()
    {
        return vendas.Listar();
    }

    public static void InformaEntregas(int id, List<Venda> vendasEntregar)
    {
        Venda x = ObterVendas(id);
        if(vendasEntregar.Exists(y => y.id == x.id))
        {
            List<ProdutoVenda> produtosVenda = ObterListaProdutosVendas(x.id).Where(x => x.recebido == false && x.resgate == false).ToList();
            // Atualizar Entrega de Produtos da Venda
            produtosVenda.ForEach(y => {y.recebido = true; produtosVendas.Atualizar(y);});
        } else throw new ArgumentException($"O Nº informado não corresponde a nenhuma Venda não entregue! Informe um valor correspondente a uma das Vendas listados. \n");
    }

    public static void VerificarIdVendas(int id)
    {
        // Encontrar a Venda cujo ID de Cadastro foi Informado
        if(ObterVendas(id) == null) throw new ArgumentException($"O Nº informado não corresponde a nenhuma Venda! Informe um valor correspondente a uma das Vendas listadas. \n");
    }

    public static List<Venda> ObterListaVendasEntregar()
    {
        List<Venda> vendasEntrega = ListarVendas().Where(x => !x.carrinho && x.frete != 0).ToList();
        vendasEntrega.ToList().ForEach(x => 
        {
            List<ProdutoVenda> produtosVenda = ObterListaProdutosVendas(x.id);
            bool venda = false;
            produtosVenda.ForEach(x => {
                if(x.recebido == false && x.resgate == false)
                {
                    venda = true;
                }
            });
            
            if(!venda) vendasEntrega.Remove(x);
        });

        return vendasEntrega;
    }

    public static Venda ObterVendas(int id)
    {
        return vendas.ListarId(id);
    }

    public static void ValidarDadosFormasPagamento(string descricao, int parcelas, double percentual, int diaVencimento)
    {
        // Evitar acumulo de erros anteriores
        erros.Clear();

        // Verificação de Descrição das Formas de Pagamento
        if(string.IsNullOrWhiteSpace(descricao)) erros.Add("\tO campo Descrição deve ser preenchido!");

        // Verificação da Quantidade de Parcelas das Formas de Pagamento
        if(parcelas < 1) erros.Add("\tA Forma de Pagamento deve possui pelo menos uma Parcela!");

        // Verificação do Percentual de Desconto/Juros das Formas de Pagamento
        if(percentual < -15 && percentual > 10) erros.Add("\tA Forma de Pagamento pode ter no mínimo 15% de Desconto e no máximo 10% de Juros!");

        // Verificação da Quantidade de Dias para o Vencimento na Forma de Pagamento
        if(diaVencimento < 0) erros.Add("\tA Quantidade de Dias para o Vencimento não pode ser um valor negativo!");

        // Verificar se há erros
        if(erros.Count > 0) throw new ArgumentException($"\n----- Cadastro Não Realizado! Erros Encontrados ----- \n{string.Join(Environment.NewLine, erros)} \n");
    }

    public static void InserirFormasPagamento(string descricao, int parcelas, double percentual, int diaVencimento)
    {
        // Validação de Dados da Forma de Pagamento
        ValidarDadosFormasPagamento(descricao, parcelas, percentual, diaVencimento);

        // Inserir o Cadastro da Forma de Pagamento
        FormaPagamento x = new FormaPagamento(0, descricao, parcelas, percentual, diaVencimento);
        formasPagamento.Inserir(x);
    }

    public static void RemoverFormasPagamento(int id)
    {
        // Forma de Pagamento cujo Cadastro será removido
        FormaPagamento x = ObterFormasPagamento(id);

        // Remover o Cadastro da Forma de Pagamento
        formasPagamento.Remover(x);
    }

    public static void AtualizarFormasPagamento(int id, string descricao, int parcelas, double percentual, int diaVencimento)
    {
        // Forma de Pagamento cujo Cadastro será atualizado
        FormaPagamento x = ObterFormasPagamento(id);

        // Atualização de Dados do Cadastro da Forma de Pagamento
        if(!string.IsNullOrEmpty(descricao) || (parcelas != x.parcelas && parcelas != -1) || (percentual != x.percentual && percentual != -1) || (diaVencimento != x.diaVencimento && diaVencimento != -1))
        {
            if(!string.IsNullOrEmpty(descricao)) x.descricao = descricao;
            if(parcelas != x.parcelas && parcelas != -1) x.parcelas = parcelas;
            if(percentual != x.percentual && percentual != -1) x.percentual = percentual;
            if(diaVencimento != x.diaVencimento && diaVencimento != -1) x.diaVencimento = diaVencimento;
        }

        // Validação de Dados da Forma de Pagamento
        ValidarDadosFormasPagamento(x.descricao, x.parcelas, x.percentual, x.diaVencimento);

        // Atualizar o Cadastro da Forma de Pagamento
        formasPagamento.Atualizar(x);
    }

    public static List<FormaPagamento> ListarFormasPagamento()
    {
        return formasPagamento.Listar();
    }

    public static void VerificarIdFormasPagamento(int id)
    {
         // Encontrar o Produto cujo ID de Cadastro foi Informado
        if(ObterFormasPagamento(id) == null) throw new ArgumentException($"O Nº informado não corresponde a nenhuma Forma de Pagamento! Informe um valor correspondente a uma das Formas de Pagamento listados. \n");
    }

    public static FormaPagamento ObterFormasPagamento(int id)
    {
        return formasPagamento.ListarId(id);
    }

    public static List<IGrouping<int, Venda>> ObterVendasGeral()
    {
        // Obter a Lista de todas as Vendas Fechadas de todos os Clientes
        return ListarVendas().Where(x => !x.carrinho).GroupBy(x => x.idCliente).ToList();
    }

    public static IGrouping<int, Venda> ObterVendasCliente(List<IGrouping<int, Venda>> vendasClientes, int idCliente)
    {
        // Obter um Agrupamento de todas as Vendas de um Cliente
        return vendasClientes.FirstOrDefault(x => x.Key == idCliente);
    }
}