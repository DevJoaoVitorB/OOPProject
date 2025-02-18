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
            if(logado == false) sair = Login();
            else 
            {
                View.CriarAdmin();
                sair = Menu();

                switch (sair)
                {
                    case -1:
                        Console.WriteLine("O valor informado é inválido para essa Operação! \n");
                        break;
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
                        Console.WriteLine("A Operação informada não existe! Informe valores de 0 a 8. \n");
                        break;
                }
            }
        } while(sair != 0);
    }

    public static int Login()
    {
        Console.WriteLine("Bem-Vindo a Loja Virtual de Jogos!");
        Console.WriteLine("\n\t----- LOGIN -----");
        Console.WriteLine("\n\t[1]Entrar [0]Sair \n");
        Console.Write("Opção: ");
        int valor;
        if(int.TryParse(Console.ReadLine(), out valor))
        {
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
                    Console.WriteLine("A Operação informada não existe! Informe os valores 0 ou 1. \n");
                    return valor;
            }
        } else {Console.WriteLine("O valor informado é inválido para essa Operação! \n"); return -1;}
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
        int valor;
        if(int.TryParse(Console.ReadLine(), out valor))
        {
            Console.Clear();
            if(valor == 0) return -1;
            return valor;
        } else return -1;
    }
}

static class ManterUsuarios
{
    public static void MenuUsuarios()
    {
        int valor = -1;
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
                if(int.TryParse(Console.ReadLine(), out valor))
                {
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
                            Console.WriteLine("A Operação informada não existe! Informe valores de 0 a 8. \n");
                            break;
                    } 
                } else {Console.WriteLine("O valor informado é inválido para essa Operação! \n"); valor = -1;}
            } 
            catch (ArgumentException ex) {Console.WriteLine(ex.Message);}
            catch (InvalidOperationException ex) {Console.WriteLine(ex.Message);}
        } while(valor != 0);
    }

    public static void InserirCliente()
    {
        Console.WriteLine("\n --- Informações de Cadastro --- \n");
        Console.Write("Informe o Nome do Cliente: ");
        string nome = Console.ReadLine();
        Console.Write("Informe o E-mail do Cliente: ");
        string email = Console.ReadLine();
        Console.Write("Informe a Senha do Cliente (A Senha deve conter 8 Caracteres): ");
        string senha = Console.ReadLine();
        Console.Write("Informe o Endereço: ");
        string endereco = Console.ReadLine();
        Console.Write("Informe o CEP do Cliente (XXXXXXXX): ");
        string cep = Console.ReadLine();
        Console.Write("Informe o CPF do Cliente (XXXXXXXXXXX): ");
        string cpf = Console.ReadLine();

        // Operação para Cadastrar o Cliente
        View.InserirUsuarios(nome, email, senha, endereco, cep, cpf, false);
    }

    public static void RemoverCliente()
    {
        ListarClientes();
        Console.Write("Informe o N° do Cadastro do Cliente a ser Removido: ");
        int id;
        if(int.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            View.VerificarIdUsuarios(id, false);

            // Operação para Remover o Cadastro do Cliente
            View.RemoverUsuarios(id);
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
    }

    public static void AtualizarCliente()
    {
        ListarClientes();
        Console.Write("Informe o N° do Cadastro do Cliente a ser Atualizado: ");
        int id;
        if(int.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            View.VerificarIdUsuarios(id, false);

            string nome, email, senha, endereco, cep, cpf;
            nome = email = senha = endereco = cep = cpf = "";
            int alterar = -1;

            do
            {
                Console.WriteLine("\t----- O Que Deseja Alterar no Cadastro? -----");
                Console.WriteLine("[1]Nome [2]E-mail [3]Senha [4]Endereço [5]CEP [6]CPF [0]Sair \n");
                Console.Write("Opção: ");
                if(int.TryParse(Console.ReadLine(), out alterar)){
                    switch (alterar)
                    {
                        case 0:
                            break;
                        case 1:
                            Console.Write("\nInforme o Novo Nome do Cliente: ");
                            nome = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("\nInforme o Novo E-Mail do Cliente: ");
                            email = Console.ReadLine();
                            break;
                        case 3:
                            Console.Write("\nInforme a Nova Senha do Cliente (A Nova Senha deve conter 8 Caracteres): ");
                            senha = Console.ReadLine();
                            break;
                        case 4:
                            Console.Write("\nInforme o Novo Endereço do Cliente: ");
                            endereco = Console.ReadLine();
                            break;
                        case 5:
                            Console.Write("\nInforme o Novo CEP do Cliente (XXXXXXXX): ");
                            cep = Console.ReadLine();
                            break;
                        case 6:
                            Console.Write("\nInforme o Novo CPF do Cliente (XXXXXXXXXXX): ");
                            cpf = Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("\nA Operação informada não existe! Informe valores de 0 a 6. \n");
                            break;
                    }
                } else {Console.WriteLine("O valor informado é inválido para essa Operação! \n"); alterar = -1;}
            } while (alterar != 0);

            // Operação para Atualizar o Cadastro do Cliente
            View.AtualizarUsuarios(id, nome, email, senha, endereco, cep, cpf, false);
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
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
        Console.Write("Informe o Nome do Admin: ");
        string nome = Console.ReadLine();
        Console.Write("Informe o E-mail do Admin: ");
        string email = Console.ReadLine();
        Console.Write("Informe a Senha do Admin (A Senha deve conter 8 Caracteres): ");
        string senha = Console.ReadLine();

        // Operação para Cadastrar o Admin
        View.InserirUsuarios(nome, email, senha, "", "", "", true);
    }

    public static void RemoverAdmin()
    {
        ListarAdmins();
        Console.Write("Informe o N° do Cadastro do Admin a ser Removido: ");
        int id;
        if(int.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            View.VerificarIdUsuarios(id, true);

            // Operação para Remover o Cadastro do Cliente
            View.RemoverUsuarios(id);
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
    }

    public static void AtualizarAdmin()
    {
        ListarAdmins();
        Console.Write("Informe o N° do Cadastro do Admin a ser Atualizado: ");
        int id;
        if(int.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            View.VerificarIdUsuarios(id, true);

            string nome, email, senha;
            nome = email = senha = "";
            int alterar = -1;

            do
            {
                Console.WriteLine("\t----- O Que Deseja Alterar no Cadastro? -----");
                Console.WriteLine("\t     [1]Nome [2]E-mail [3]Senha [0]Sair \n");
                Console.Write("Opção: ");
                if(int.TryParse(Console.ReadLine(), out alterar))
                {
                    switch (alterar)
                    {
                        case 0:
                            break;
                        case 1:
                            Console.Write("\nInforme o Novo Nome do Admin: ");
                            nome = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("\nInforme o Novo E-Mail do Admin: ");
                            email = Console.ReadLine();
                            break;
                        case 3:
                            Console.Write("\nInforme a Nova Senha do Admin (A Nova Senha deve conter 8 Caracteres): ");
                            senha = Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("\nA Operação informada não existe! Informe valores de 0 a 3. \n");
                            break;
                    }
                } else {Console.WriteLine("O valor informado é inválido para essa Operação! \n"); alterar = -1;}
            } while (alterar != 0);

            // Operação para Atualizar o Cadastro do Admin
            View.AtualizarUsuarios(id, nome, email, senha, "", "", "", true);
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
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
        int valor = -1;
        do{
            Console.WriteLine("\n\t------- Gerenciamento de Categorias ------- \n");
            Console.WriteLine("[1]Realizar Cadastro de Categoria  [3]Atualizar Cadastro de Categoria");
            Console.WriteLine("[2]Remover Cadastro de Categoria   [4]Listar Cadastros de Categorias");
            Console.WriteLine("[0]Sair \n");
            Console.Write("Opção: ");
            try
            {
                if(int.TryParse(Console.ReadLine(), out valor))
                {
                    Console.Clear();
                    switch (valor)
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
                            Console.WriteLine("A Operação informada não existe! Informe valores de 0 a 4. \n");
                            break;
                    }
                } else {Console.WriteLine("O valor informado é inválido para essa Operação! \n"); valor = -1;}
            } 
            catch (ArgumentException ex) {Console.WriteLine(ex.Message);}
            catch (InvalidOperationException ex) {Console.WriteLine(ex.Message);}
        } while(valor != 0);
    }

    public static void InserirCategoria()
    {
        Console.WriteLine("\n --- Informações de Cadastro --- \n");
        Console.Write("Informe a Descrição da Categoria: ");
        string descricao = Console.ReadLine();
        Console.Write("Informe o Desconto da Categoria (%): ");
        double desconto;
        if(double.TryParse(Console.ReadLine(), out desconto))
        {
            // Operação para Cadastrar Categoria
            View.InserirCategorias(descricao, desconto);
        } else Console.WriteLine("O valor de Desconto da Categoria deve ser numérico! \n"); 
    }

    public static void RemoverCategoria()
    {
        ListarCategorias();
        Console.Write("Informe o N° do Cadastro da Categoria a ser Removida: ");
        int id;
        if(int.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            View.VerificarIdCategorias(id);

            // Operação para Remover o Cadastro da Categoria
            View.RemoverCategorias(id);
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
    }

    public static void AtualizarCategoria()
    {
        ListarCategorias();
        Console.Write("Informe o N° do Cadastro da Categoria a ser Atualizada: ");
        int id;
        if(int.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            View.VerificarIdCategorias(id);

            string descricao = ""; 
            double desconto = -1;
            int alterar = -1;

            do
            {
                Console.WriteLine("\t----- O Que Deseja Alterar no Cadastro? -----");
                Console.WriteLine("\t      [1]Descrição [2]Desconto [0]Sair \n");
                Console.Write("Opção: ");
                if(int.TryParse(Console.ReadLine(), out alterar))
                {
                    switch (alterar)
                    {
                        case 0:
                            break;
                        case 1:
                            Console.Write("\nInforme a Nova Descrição da Categoria: ");
                            descricao = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("\nInforme o Novo Desconto da Categoria (%): ");
                            if(!double.TryParse(Console.ReadLine(), out desconto)) {Console.WriteLine("O valor de Desconto da Categoria deve ser numérico! \n"); desconto = -1;} 
                            break;
                        default:
                            Console.WriteLine("\nA Operação informada não existe! Informe valores de 0 a 2. \n");
                            break;
                    }
                } else {Console.WriteLine("O valor informado é inválido para essa Operação! \n"); alterar = -1;}
            } while (alterar != 0);

            // Operação para Atualizar o Cadastro do Admin
            View.AtualizarCategorias(id, descricao, desconto);
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
    }

    public static void ListarCategorias()
    {
        if(View.ListarCategorias().Any())
        {
            Console.WriteLine("\n----- Cadastro de Categorias ----- \n"); 
            foreach(Categoria x in View.ListarCategorias()) Console.WriteLine(x);
        } else throw new InvalidOperationException("Não há nenhuma Categoria cadastrada! \n");
    }
}

static class ManterProdutos
{
    public static void MenuProdutos()
    {
        int valor = -1;
        do{
            Console.WriteLine("\n\t------- Gerenciamento de Produtos ------- \n");
            Console.WriteLine("[1]Realizar Cadastro de Produto  [4]Listar Cadastros de Produtos");
            Console.WriteLine("[2]Remover Cadastro de Produto   [5]Reajuste de Preço de Produto");
            Console.WriteLine("[3]Atualizar Cadastro de Produto");
            Console.WriteLine("[0]Sair \n");
            Console.Write("Opção: ");
            try
            {
                if(int.TryParse(Console.ReadLine(), out valor))
                {
                    Console.Clear();
                    switch (valor)
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
                            Console.WriteLine("A Operação informada não existe! Informe valores de 0 a 5. \n");
                            break;
                    }
                } else {Console.WriteLine("O valor informado é inválido para essa Operação! \n"); valor = -1;}
            }
            catch (ArgumentException ex) {Console.WriteLine(ex.Message);}
            catch (InvalidOperationException ex) {Console.WriteLine(ex.Message);}
        } while(valor != 0);
    }

    public static void InserirProduto()
    {
        ManterCategorias.ListarCategorias();
        Console.Write("Informe o Nº do Cadastro da Categoria deste Produto: ");
        int idCategoria;
        if(int.TryParse(Console.ReadLine(), out idCategoria))
        {
            Console.Clear();
            View.VerificarIdCategorias(idCategoria);

            Console.WriteLine("\n --- Informações de Cadastro --- \n");
            Console.Write("Informe a Descrição do Produto: ");
            string descricao = Console.ReadLine();
            Console.Write("Informe o Preço do Produto: R$ ");
            double preco;
            if(double.TryParse(Console.ReadLine(), out preco))
            {
                Console.Write("Informe a Quantidade em Estoque do Produto: ");
                int estoque;
                if(int.TryParse(Console.ReadLine(), out estoque))
                {
                    int valor = -1;
                    bool digital = true;
                    do{
                        Console.WriteLine("Digite - [1]Produto Digital [2]Produto Físico");
                        Console.Write("Opção: ");
                        if(int.TryParse(Console.ReadLine(), out valor))
                        {
                            switch (valor)
                            {
                                case 1:
                                    break;
                                case 2:
                                    digital = false;
                                    break;
                                default:
                                    Console.WriteLine("A Operação informada não existe! Informe os valores 1 ou 2. \n");
                                    break;
                            }
                        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
                    } while(valor != 1 && valor != 2);

                    // Operação para Cadastrar Categoria
                    View.InserirProdutos(descricao, preco, estoque, digital, idCategoria);
                } else Console.WriteLine("A Quantidade do Produto em Estoque deve ser númerica! \n");
            } else Console.WriteLine("O Preço do Produto deve ser númerico! \n");
        } else Console.WriteLine("O valor informado não corresponde a um ID de Categoria! Informe um valor correspondente a uma das Categorias listadas. \n");
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