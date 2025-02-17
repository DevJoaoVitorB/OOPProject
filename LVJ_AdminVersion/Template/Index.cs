AdminUI.MainUI();

static class AdminUI
{
    private static bool logado;
    private static int idLogado;

    public static void MainUI()
    {
        View.CriarAdmin();
        int sair = 1;

        do{
            try
            {
                if(logado == false) sair = Login();
                else 
                {
                    View.CriarAdmin();
                    sair = Menu();

                    switch (sair)
                    {
                        case 0:
                            break;
                        case 1:
                            ManterUsuarios.MenuUsuarios();
                            break;
                        case 2:
                            ManterCategorias.MenuCategorias();
                            break;
                        case 3:
                            ManterProdutos.MenuProdutos();
                            break;
                        case 4:
                            ManterVendas.MenuVendas();
                            break;
                        case 5:
                            ManterFormasPagamento.MenuFormasPagamento();
                            break;
                        case 6:
                            Relatorios.MenuRelatorios();
                            break;
                        case 7:
                            StatusPedidos.MenuStatusPedidos();
                            break;
                        case 8:
                            logado = false;
                            break;
                        default:
                            Console.WriteLine("A Operação informada não existe! \n");
                            break;
                    }
                }
            } 
            catch (FormatException) {Console.WriteLine("Valor informado inválido para essa Operação! \n");}
            catch (ArgumentException ex) {Console.WriteLine(ex.Message);}
        } while(sair != 0);
    }

    public static int Login()
    {
        Console.WriteLine("Bem-Vindo a Loja Virtual de Jogos!");
        Console.WriteLine("\n\t----- LOGIN -----");
        Console.WriteLine("\n\t[1]Entrar [0]Sair \n");
        Console.Write("Opção: ");
        int valor = int.Parse(Console.ReadLine());
        Console.Clear();

        switch (valor)
        {
            case 0:
                return 0;
            case 1:
                Console.Write("E-mail: ");
                string email = Console.ReadLine();
                Console.Write("Senha: ");
                string senha = Console.ReadLine();
                Console.Clear();
                logado = View.ValidarLogin(email, senha, out idLogado);
                if(logado) Console.WriteLine("Login feito com sucesso! \n");
                else Console.WriteLine("Login não efetuado! E-mail ou Senha incorretos! \n");
                return 1;
            default:
                Console.WriteLine("A Operação informada não existe! \n");
                return valor;
        }
    }

    public static int Menu()
    {
        foreach(Usuario x in View.ListarUsuarios()) if(idLogado == x.id) Console.WriteLine($"Bem-Vindo Administrador {x.nome}");
        Console.WriteLine("\n\t------- Menu de Administrador ------- \n");
        Console.WriteLine("[1]Gerenciamento de Usuários   [5]Gerenciamento de Formas de Pagamento");
        Console.WriteLine("[2]Gerenciamento de Categorias [6]Relatórios de Vendas");
        Console.WriteLine("[3]Gerenciamento de Produtos   [7]Gerenciamento de Status de Pedidos");
        Console.WriteLine("[4]Gerenciamento de Vendas     [8]Logout\n");
        Console.Write("Opção: ");
        int valor = int.Parse(Console.ReadLine());
        Console.Clear();
        if(valor == 0) return 9;
        return valor;
    }
}

static class ManterUsuarios
{
    public static void MenuUsuarios()
    {
        int valor = 1;
        do{
            Console.WriteLine("\n\t------- Gerenciamento de Usuários ------- \n");
            Console.WriteLine("[1]Realizar Cadastro de Cliente  [5]Realizar Cadastro de Admin");
            Console.WriteLine("[2]Remover Cadastro de Cliente   [6]Remover Cadastro de Admin");
            Console.WriteLine("[3]Atualizar Cadastro de Cliente [7]Atualizar Cadastro de Admin");
            Console.WriteLine("[4]Listar Cadastros de Clientes  [8]Listar Cadastros de Admins");
            Console.WriteLine("[0]Sair \n");
            Console.Write("Opção: ");
            try
            {
                valor = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (valor)
                {
                    case 0:
                        break;
                    case 1:
                        InserirCliente();
                        break;
                    case 2:
                        RemoverCliente();
                        break;
                    case 3:
                        AtualizarCliente();
                        break;
                    case 4:
                        ListarClientes();
                        break;
                    case 5:
                        InserirAdmin();
                        break;
                    case 6:
                        RemoverAdmin();
                        break;
                    case 7:
                        AtualizarAdmin();
                        break;
                    case 8:
                        ListarAdmins();
                        break;
                    default:
                        Console.WriteLine("A Operação informada não existe! \n");
                        break;
                }
            } 
            catch (FormatException) {Console.WriteLine("Valor informado é inválido para essa Operação! \n");}
            catch (ArgumentException ex) {Console.WriteLine(ex.Message);}
            catch (InvalidOperationException ex) {Console.WriteLine(ex.Message);}
        } while(valor != 0);
    }

    public static void InserirCliente()
    {
        Console.WriteLine("\n --- Informações de Cadastro --- \n");
        Console.Write("Informe o Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Informe o E-mail: ");
        string email = Console.ReadLine();
        Console.Write("Informe a Senha (A Senha deve conter 8 Caracteres): ");
        string senha = Console.ReadLine();
        Console.Write("Informe o Endereço: ");
        string endereco = Console.ReadLine();
        Console.Write("Informe o CEP (XXXXXXXX): ");
        string cep = Console.ReadLine();
        Console.Write("Informe o CPF (XXXXXXXXXXX): ");
        string cpf = Console.ReadLine();

        // Operação para Cadastrar o Cliente
        View.InserirUsuarios(nome, email, senha, endereco, cep, cpf, false);
    }

    public static void RemoverCliente()
    {
        ListarClientes();
        Console.Write("Informe o N° do Cadastro do Cliente a ser Removido: ");
        int id = int.Parse(Console.ReadLine());
        Console.Clear();
        View.VerificarIdUsuarios(id, false);

        // Operação para Remover o Cadastro do Cliente
        View.RemoverUsuarios(id);
    }

    public static void AtualizarCliente()
    {
        ListarClientes();
        Console.Write("Informe o N° do Cadastro do Cliente a ser Atualizado: ");
        int id = int.Parse(Console.ReadLine());
        Console.Clear();
        View.VerificarIdUsuarios(id, false);

        string nome, email, senha, endereco, cep, cpf;
        nome = email = senha = endereco = cep = cpf = "";
        int alterar = 1;

        do
        {
            try
            {
                Console.WriteLine("\t----- O Que Deseja Alterar no Cadastro? -----");
                Console.WriteLine("[1]Nome [2]E-mail [3]Senha [4]Endereço [5]CEP [6]CPF [0]Sair \n");
                Console.Write("Opção: ");
                alterar = int.Parse(Console.ReadLine());
                
                switch (alterar)
                {
                    case 0:
                        break;
                    case 1:
                        Console.Write("\nInforme o Novo Nome: ");
                        nome = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("\nInforme o Novo E-Mail: ");
                        email = Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("\nInforme a Nova Senha (A Nova Senha deve conter 8 Caracteres): ");
                        senha = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("\nInforme o Novo Endereço: ");
                        endereco = Console.ReadLine();
                        break;
                    case 5:
                        Console.Write("\nInforme o Novo CEP (XXXXXXXX): ");
                        cep = Console.ReadLine();
                        break;
                    case 6:
                        Console.Write("\nInforme o Novo CPF (XXXXXXXXXXX): ");
                        cpf = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("\nA Operação informada não existe! \n");
                        break;
                }
            } catch (FormatException) {Console.WriteLine("\nValor informado é inválido para essa Operação! \n");}
        } while (alterar != 0);

        // Operação para Atualizar o Cadastro do Cliente
        View.AtualizarUsuarios(id, nome, email, senha, endereco, cep, cpf, false);
    }

    public static void ListarClientes()
    {
        List<Usuario> clientes = View.ListarUsuarios().Where(x => !x.admin).ToList();
        if(clientes.Any())
        {
            Console.WriteLine("\n----- Cadastro de Clientes ----- \n"); 
            foreach(Usuario x in clientes) Console.WriteLine(x);
        } else throw new InvalidOperationException("Não há nenhum Cliente cadastrado! \n");
    }

    public static void InserirAdmin()
    {
        Console.WriteLine("\n --- Informações de Cadastro --- \n");
        Console.Write("Informe o Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Informe o E-mail: ");
        string email = Console.ReadLine();
        Console.Write("Informe a Senha (A Senha deve conter 8 Caracteres): ");
        string senha = Console.ReadLine();

        // Operação para Cadastrar o Admin
        View.InserirUsuarios(nome, email, senha, "", "", "", true);
    }

    public static void RemoverAdmin()
    {
        ListarAdmins();
        Console.Write("Informe o N° do Cadastro do Admin a ser Removido: ");
        int id = int.Parse(Console.ReadLine());
        Console.Clear();
        View.VerificarIdUsuarios(id, true);

        // Operação para Remover o Cadastro do Admin
        View.RemoverUsuarios(id);
    }

    public static void AtualizarAdmin()
    {
        ListarAdmins();
        Console.Write("Informe o N° do Cadastro do Admin a ser Atualizado: ");
        int id = int.Parse(Console.ReadLine());
        Console.Clear();
        View.VerificarIdUsuarios(id, true);

        string nome, email, senha;
        nome = email = senha = "";
        int alterar = 1;

        do
        {
            try
            {
                Console.WriteLine("\t----- O Que Deseja Alterar no Cadastro? -----");
                Console.WriteLine("\t     [1]Nome [2]E-mail [3]Senha [0]Sair \n");
                Console.Write("Opção: ");
                alterar = int.Parse(Console.ReadLine());
                
                switch (alterar)
                {
                    case 0:
                        break;
                    case 1:
                        Console.Write("\nInforme o Novo Nome: ");
                        nome = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("\nInforme o Novo E-Mail: ");
                        email = Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("\nInforme a Nova Senha (A Nova Senha deve conter 8 Caracteres): ");
                        senha = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("\nA Operação informada não existe! \n");
                        break;
                }
            } catch (FormatException) {Console.WriteLine("\nValor informado é inválido para essa Operação! \n");}
        } while (alterar != 0);

        // Operação para Atualizar o Cadastro do Admin
        View.AtualizarUsuarios(id, nome, email, senha, "", "", "", true);
    }

    public static void ListarAdmins()
    {
        List<Usuario> clientes = View.ListarUsuarios().Where(x => x.admin).ToList();
        if(clientes.Any())
        {
            Console.WriteLine("\n----- Cadastro de Administradores ----- \n"); 
            foreach(Usuario x in clientes) Console.WriteLine(x);
        } else throw new InvalidOperationException("Não há nenhum Administrador cadastrado! \n");
    }
}

static class ManterCategorias
{
    public static void MenuCategorias()
    {
        int value = 1;
        do{
            Console.WriteLine("\n\t------- Gerenciamento de Categorias ------- \n");
            Console.WriteLine("[1]Realizar Cadastro de Categoria  [3]Atualizar Cadastro de Categoria");
            Console.WriteLine("[2]Remover Cadastro de Categoria   [4]Listar Cadastros de Categorias");
            Console.WriteLine("[0]Sair \n");
            Console.Write("Opção: ");
            try
            {
                value = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (value)
                {
                    case 0:
                        break;
                    case 1:
                        InserirCategoria();
                        break;
                    case 2:
                        RemoverCategoria();
                        break;
                    case 3:
                        AtualizarCategoria();
                        break;
                    case 4:
                        ListarCategorias();
                        break;
                    default:
                        Console.WriteLine("A Operação informada não existe! \n");
                        break;
                }
            } catch (Exception) {
                Console.WriteLine("Valor informado inválido para essa Operação! \n");
            }
        } while(value != 0);
    }

    public static void InserirCategoria()
    {

    }

    public static void RemoverCategoria()
    {

    }

    public static void AtualizarCategoria()
    {

    }

    public static void ListarCategorias()
    {

    }
}

static class ManterProdutos
{
    public static void MenuProdutos()
    {
        int value = 1;
        do{
            Console.WriteLine("\n\t------- Gerenciamento de Produtos ------- \n");
            Console.WriteLine("[1]Realizar Cadastro de Produto  [4]Listar Cadastros de Produtos");
            Console.WriteLine("[2]Remover Cadastro de Produto   [5]Reajuste de Preço de Produto");
            Console.WriteLine("[3]Atualizar Cadastro de Produto");
            Console.WriteLine("[0]Sair \n");
            Console.Write("Opção: ");
            try
            {
                value = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (value)
                {
                    case 0:
                        break;
                    case 1:
                        InserirProduto();
                        break;
                    case 2:
                        RemoverProduto();
                        break;
                    case 3:
                        AtualizarProduto();
                        break;
                    case 4:
                        ListarProdutos();
                        break;
                    case 5:
                        ReajustePreco();
                        break;
                    default:
                        Console.WriteLine("A Operação informada não existe! \n");
                        break;
                }
            } catch (Exception) {
                Console.WriteLine("Valor informado inválido para essa Operação! \n");
            }
        } while(value != 0);
    }

    public static void InserirProduto()
    {

    }

    public static void RemoverProduto()
    {

    }

    public static void AtualizarProduto()
    {

    }

    public static void ListarProdutos()
    {
        
    }

    public static void ReajustePreco()
    {

    }
}

static class ManterVendas
{
    public static void MenuVendas()
    {
        int value = 1;
        do{
            Console.WriteLine("\n\t------- Gerenciamento de Vendas ------- \n");
            Console.WriteLine("[1]Remover Cadastro de Venda   [4]Remover Produto de Venda");
            Console.WriteLine("[2]Atualizar Cadastro de Venda [5]Atualizar Produto de Venda");
            Console.WriteLine("[3]Listar Cadastros de Vendas");
            Console.WriteLine("[0]Sair \n");
            Console.Write("Opção: ");
            try
            {
                value = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (value)
                {
                    case 0:
                        break;
                    case 1:
                        RemoverVenda();
                        break;
                    case 2:
                        AtualizarVenda();
                        break;
                    case 3:
                        ListarVendas();
                        break;
                    case 4:
                        RemoverProdutoVenda();
                        break;
                    case 5:
                        AtualizarProdutoVenda();
                        break;
                    default:
                        Console.WriteLine("A Operação informada não existe! \n");
                        break;
                }
            } catch (Exception) {
                Console.WriteLine("Valor informado inválido para essa Operação! \n");
            }
        } while(value != 0);
    }

    public static void RemoverVenda()
    {

    }

    public static void AtualizarVenda()
    {

    }

    public static void ListarVendas()
    {

    }

    public static void RemoverProdutoVenda()
    {

    }

    public static void AtualizarProdutoVenda()
    {

    }

}

static class ManterFormasPagamento
{
    public static void MenuFormasPagamento()
    {
        int value = 1;
        do{
            Console.WriteLine("\n\t---------- Gerenciamento de Formas de Pagamento ---------- \n");
            Console.WriteLine("[1]Realizar Cadastro de Forma de Pagamento  [3]Atualizar Cadastro de Forma de Pagamento");
            Console.WriteLine("[2]Remover Cadastro de Forma de Pagamento   [4]Listar Cadastros de Formas de Pagamento");
            Console.WriteLine("[0]Sair \n");
            Console.Write("Opção: ");
            try
            {
                value = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (value)
                {
                    case 0:
                        break;
                    case 1:
                        InserirFormaPagamento();
                        break;
                    case 2:
                        RemoverFormaPagamento();
                        break;
                    case 3:
                        AtualizarFormaPagamento();
                        break;
                    case 4:
                        ListarFormasPagamento();
                        break;
                    default:
                        Console.WriteLine("A Operação informada não existe! \n");
                        break;
                }
            } catch (Exception) {
                Console.WriteLine("Valor informado inválido para essa Operação! \n");
            }
        } while(value != 0);
    }

    public static void InserirFormaPagamento()
    {

    }

    public static void RemoverFormaPagamento()
    {

    }

    public static void AtualizarFormaPagamento()
    {

    }

    public static void ListarFormasPagamento()
    {

    }
}

static class Relatorios
{
    public static void MenuRelatorios()
    {
        int value = 1;
        do{
            Console.WriteLine("\n\t------- Gerenciamento de Relatório de Vendas ------- \n");
            Console.WriteLine("[1]Visualizar Relatório de Venda Parcial [2]Visualizar Relatório de Venda Total");
            Console.WriteLine("[0]Sair \n");
            Console.Write("Opção: ");
            try
            {
                value = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (value)
                {
                    case 0:
                        break;
                    case 1:
                        RelatorioParcial();
                        break;
                    case 2:
                        RelatorioTotal();
                        break;
                    default:
                        Console.WriteLine("A Operação informada não existe! \n");
                        break;
                }
            } catch (Exception) {
                Console.WriteLine("Valor informado inválido para essa Operação! \n");
            }
        } while(value != 0);
    }

    public static void RelatorioParcial()
    {

    }

    public static void RelatorioTotal()
    {

    }
}

static class StatusPedidos
{
    public static void MenuStatusPedidos()
    {
        int value = 1;
        do{
            Console.WriteLine("\n\t---- Gerenciamento de Status de Pedidos ---- \n");
            Console.WriteLine("[1]Informa Entrega de Produto [2]Informa Resgate de Produto");
            Console.WriteLine("[0]Sair \n");
            Console.Write("Opção: ");
            try
            {
                value = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (value)
                {
                    case 0:
                        break;
                    case 1:
                        InformaEntrega();
                        break;
                    case 2:
                        InformaResgate();
                        break;
                    default:
                        Console.WriteLine("A Operação informada não existe! \n");
                        break;
                }
            } catch (Exception) {
                Console.WriteLine("Valor informado inválido para essa Operação! \n");
            }
        } while(value != 0);
    }
    
    public static void InformaEntrega()
    {
        
    }

    public static void InformaResgate()
    {

    }
}