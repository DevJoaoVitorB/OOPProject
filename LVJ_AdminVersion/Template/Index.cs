using System.Globalization;

AdminUI.MainUI();

static class AdminUI
{
    private static bool logado;
    private static int idLogado;

    public static void MainUI()
    {
        View.CriarAdmin();
        int sair = -1;

        do{
            if(logado == false) sair = Login();
            else 
            {
                View.CriarAdmin();
                sair = Menu();

                switch (sair)
                {
                    case -1:
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
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n"); return -1;
    }

    public static int Menu()
    {
        Usuario logado = View.ObterUsuarios(idLogado);
        Console.WriteLine($"Bem-Vindo Administrador {logado.nome}");
        Console.WriteLine("\n\t------- Menu de Administrador ------- \n");
        Console.WriteLine("[1]Gerenciamento de Usuários   [5]Gerenciamento de Formas de Pagamento");
        Console.WriteLine("[2]Gerenciamento de Categorias [6]Relatórios de Vendas");
        Console.WriteLine("[3]Gerenciamento de Produtos   [7]Logout");
        Console.WriteLine("[4]Gerenciamento de Vendas \n");
        Console.Write("Opção: ");
        int valor;
        if(int.TryParse(Console.ReadLine(), out valor))
        {
            Console.Clear();
            if(valor == 0) return 9;
            return valor;
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n"); return -1;
    }
}

static class ManterUsuarios
{
    public static void MenuUsuarios()
    {
        int valor = -1;
        do{
            Console.WriteLine("\n\t------- Gerenciamento de Usuários ------- \n");
            Console.WriteLine("[1]Realizar Cadastro de Cliente  [5]Realizar Cadastro de Administrador");
            Console.WriteLine("[2]Remover Cadastro de Cliente   [6]Remover Cadastro de Administrador");
            Console.WriteLine("[3]Atualizar Cadastro de Cliente [7]Atualizar Cadastro de Administrador");
            Console.WriteLine("[4]Listar Cadastros de Clientes  [8]Listar Cadastros de Administradores");
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
        Console.WriteLine("\n--- Informações de Cadastro --- \n");
        Console.Write("Informe o Nome do Cliente: ");
        string nome = Console.ReadLine();
        Console.Write("Informe o E-mail do Cliente: ");
        string email = Console.ReadLine();
        Console.Write("Informe a Senha do Cliente (A Senha deve conter no mínimo 8 Caracteres): ");
        string senha = Console.ReadLine();
        Console.Write("Informe o Endereço: ");
        string endereco = Console.ReadLine();
        Console.Write("Informe o CEP do Cliente (EX.:12345678): ");
        string cep = Console.ReadLine();
        Console.Write("Informe o CPF do Cliente (EX.:11122233344): ");
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
                            Console.Write("\nInforme a Nova Senha do Cliente (A Nova Senha deve conter no mínimo 8 Caracteres): ");
                            senha = Console.ReadLine();
                            break;
                        case 4:
                            Console.Write("\nInforme o Novo Endereço do Cliente: ");
                            endereco = Console.ReadLine();
                            break;
                        case 5:
                            Console.Write("\nInforme o Novo CEP do Cliente (EX.:12345678): ");
                            cep = Console.ReadLine();
                            break;
                        case 6:
                            Console.Write("\nInforme o Novo CPF do Cliente (EX.:11122233344): ");
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
            clientes.ForEach(x => Console.WriteLine(x));
        } else throw new InvalidOperationException("Não há nenhum Cliente cadastrado! \n");
    }

    public static void InserirAdmin()
    {
        Console.WriteLine("\n--- Informações de Cadastro --- \n");
        Console.Write("Informe o Nome do Admin: ");
        string nome = Console.ReadLine();
        Console.Write("Informe o E-mail do Admin: ");
        string email = Console.ReadLine();
        Console.Write("Informe a Senha do Admin (A Senha deve conter no mínimo 8 Caracteres): ");
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

            // Operação para Remover o Cadastro do Admin
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
                            Console.Write("\nInforme a Nova Senha do Admin (A Nova Senha deve conter no mínimo 8 Caracteres): ");
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
        List<Usuario> admins = View.ListarUsuarios().Where(x => x.admin).ToList();
        if(admins.Any())
        {
            Console.WriteLine("\n----- Cadastro de Administradores ----- \n"); 
            admins.ForEach(x => Console.WriteLine(x));
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
        Console.WriteLine("\n--- Informações de Cadastro --- \n");
        Console.Write("Informe a Descrição da Categoria: ");
        string descricao = Console.ReadLine();
        Console.Write("Informe o Desconto da Categoria (%): ");
        double desconto;
        if(double.TryParse(Console.ReadLine(), out desconto))
        {
            // Operação para Cadastrar a Categoria
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

            // Operação para Atualizar o Cadastro da Categoria
            View.AtualizarCategorias(id, descricao, desconto);
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
    }

    public static void ListarCategorias()
    {
        if(View.ListarCategorias().Any())
        {
            Console.WriteLine("\n----- Cadastro de Categorias ----- \n"); 
            View.ListarCategorias().ForEach(x => Console.WriteLine(x));
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
            Console.WriteLine("[2]Remover Cadastro de Produto   [5]Reajuste de Preço de Produtos");
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

            Console.WriteLine("\n--- Informações de Cadastro --- \n");
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
                        Console.WriteLine("Digite - [1]Produto Digital [2]Produto Físico \n");
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

                    // Operação para Cadastrar o Produto
                    View.InserirProdutos(descricao, preco, estoque, digital, idCategoria);
                } else Console.WriteLine("A Quantidade do Produto em Estoque deve ser um valor númerico! \n");
            } else Console.WriteLine("O Preço do Produto deve ser um valor númerico! \n");
        } else Console.WriteLine("O valor informado não corresponde a um ID de Categoria! Informe um valor correspondente a uma das Categorias listadas. \n");
    }

    public static void RemoverProduto()
    {
        ListarProdutos();
        Console.Write("Informe o N° do Cadastro do Produto a ser Removido: ");
        int id;
        if(int.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            View.VerificarIdProdutos(id);

            // Operação para Remover o Cadastro do Produto
            View.RemoverProdutos(id);
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
    }

    public static void AtualizarProduto()
    {
        ListarProdutos();
        Console.Write("Informe o N° do Cadastro do Produto a ser Atualizado: ");
        int id;
        if(int.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            View.VerificarIdProdutos(id);

            int idCategoria = -1;
            string descricao = "";
            double preco = -1;
            int estoque = -1;
            bool digital = true;
            int chave = -1;
            int alterar = -1;

            do
            {
                Console.WriteLine("\t---- O Que Deseja Alterar no Cadastro? ----");
                Console.WriteLine("[1]Categoria [2]Descrição [3]Preço [4]Estoque [5]Tipo [0]Sair \n");
                Console.Write("Opção: ");
                if(int.TryParse(Console.ReadLine(), out alterar))
                {
                    switch (alterar)
                    {
                        case 0:
                            break;
                        case 1:
                            ManterCategorias.ListarCategorias();
                            Console.Write("\nInforme o Nº do Cadastro da Nova Categoria do Produto: ");
                            if(int.TryParse(Console.ReadLine(), out idCategoria))
                            {
                                if(View.ObterCategorias(idCategoria) != null) Console.WriteLine("O Nº informado não corresponde a nenhuma Categoria \n");
                            } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
                            break;
                        case 2:
                            Console.Write("\nInforme a Nova Descrição do Produto: ");
                            descricao = Console.ReadLine();
                            break;
                        case 3:
                            Console.Write("\nInforme o Novo Preço do Produto: R$ ");
                            if(!double.TryParse(Console.ReadLine(), out preco)) {Console.WriteLine("O Preço do Produto deve ser um valor númerico! \n"); preco = -1;}
                            break;
                        case 4:
                            Console.Write("\nInforme a Nova Quantidade em Estoque do Produto: ");
                            if(!int.TryParse(Console.ReadLine(), out estoque)) {Console.WriteLine("A Quantidade do Produto em Estoque deve ser um valor númerico! \n"); estoque = -1;}
                            break;
                        case 5:
                            Console.WriteLine("\nDigite - [1]Produto Digital [2]Produto Físico \n");
                            Console.Write("Opção: ");
                            int valor;
                            if(int.TryParse(Console.ReadLine(), out valor))
                            {
                                switch (valor)
                                {
                                    case 1:
                                        chave = 1;
                                        break;
                                    case 2:
                                        chave = 1;
                                        digital = false;
                                        break;
                                    default:
                                        Console.WriteLine("A Operação informada não existe! Informe os valores 1 ou 2. \n");
                                        break;
                                }
                            } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
                            break;
                        default:
                            Console.WriteLine("\nA Operação informada não existe! Informe valores de 0 a 5. \n");
                            break;
                    }
                } else {Console.WriteLine("O valor informado é inválido para essa Operação! \n"); alterar = -1;}
            } while (alterar != 0);

            // Operação para Atualizar o Cadastro do Produto
            View.AtualizarProdutos(id, descricao, preco, estoque, digital, idCategoria, chave);
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
    }

    public static void ListarProdutos()
    {
        if(View.ListarProdutos().Any())
        {
            Console.WriteLine("\n----- Cadastro de Produtos ----- \n");
            View.ListarProdutos().ForEach(x => Console.WriteLine($"{x}\t{View.ObterCategorias(x.idCategoria)}"));
        } else throw new InvalidOperationException("Não há nenhum Produto cadastrado! \n");
    }

    public static void ReajustePreco()
    {
        if(View.ListarProdutos().Any())
        {
            Console.Write("Informe o Valor de Reajuste de Preço dos Produtos (%): ");
            double percentual;
            if(double.TryParse(Console.ReadLine(), out percentual))
            {
                if(percentual > -50 && percentual < 50)
                {
                    Console.Clear(); 
                    percentual = percentual < 0 ? 1 - (percentual * -1 / 100) : 1 + (percentual / 100);
                    Console.WriteLine("Informe - [1]Reajuste Total [2]Reajuste por Categoria [3]Reajuste por Produto \n");
                    Console.Write("Opção: ");
                    int valor;
                    if(int.TryParse(Console.ReadLine(), out valor))
                    {
                        switch(valor)
                        {
                            case 1:
                                View.ReajusteTotalPreco(percentual);
                                break;
                            case 2:
                                ManterCategorias.ListarCategorias();
                                Console.Write("\nInforme o Nº do Cadastro da Categoria cujo Produtos terão o Reajuste no Preço: ");
                                int idCategoria;
                                if(int.TryParse(Console.ReadLine(), out idCategoria))
                                {
                                    View.VerificarIdCategorias(idCategoria);
                                    View.ReajusteCategoriaPreco(percentual, idCategoria);
                                } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
                                break;
                            case 3:
                                ListarProdutos();
                                Console.Write("\nInforme o Nº do Cadastro do Produto que terá o Reajuste no Preço: ");
                                int idProduto;
                                if(int.TryParse(Console.ReadLine(), out idProduto))
                                {
                                    View.VerificarIdProdutos(idProduto);
                                    View.ReajusteProdutoPreco(percentual, idProduto);
                                } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
                                break;
                            default:
                                Console.WriteLine("A Operação informada não existe! Informe valores de 1 a 3. \n");
                                break;
                        }
                    } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
                } else Console.WriteLine("O Percentual de Reajuste não pode ser decréscimos ou acréscimos de 50% ou mais!");
            } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
        } else throw new InvalidOperationException("Não há nenhum Produto cadastrado! \n");
    }
}

static class ManterVendas
{
    public static void MenuVendas()
    {
        int valor = -1;
        do{
            Console.WriteLine("\n\t------- Gerenciamento de Vendas ------- \n");
            Console.WriteLine("[1]Remover Cadastro de Venda  [3]Listar Cadastros de Vendas");
            Console.WriteLine("[2]Informa Entrega de Vendas");
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
                            RemoverVenda();
                            break;
                        case 2:
                            InformaEntrega();
                            break;
                        case 3:
                            ListarVendas();
                            break;
                        default:
                            Console.WriteLine("A Operação informada não existe! Informe valores de 0 a 4. \n");
                            break;
                    }
                }
            } 
            catch (ArgumentException ex) {Console.WriteLine(ex.Message);}
            catch (InvalidOperationException ex) {Console.WriteLine(ex.Message);}
        } while(valor != 0);
    }

    public static void RemoverVenda()
    {
        ListarVendas();
        Console.Write("Informe o N° do Cadastro da Venda a ser Removida: ");
        int id;
        if(int.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            View.VerificarIdVendas(id);

            // Operação para Remover o Cadastro da Venda
            View.RemoverVendas(id);
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
    }

    public static void ListarVendas()
    {
        if(View.ListarVendas().Any())
        {
            Console.WriteLine("\n\t\t----- Cadastro de Vendas ----- \n");
            Console.WriteLine("------------------------------------------------------------------- \n");
            View.ListarVendas().ForEach(x => 
            {
                List<ProdutoVenda> produtosVendaCliente = View.ObterListaProdutosVendas(x.id);
                Console.Write(x);
                Console.WriteLine($"\tCliente: {View.ListarUsuarios().SingleOrDefault(y => x.idCliente == y.id).nome} \n");
                if(produtosVendaCliente.Any())
                {
                    Console.WriteLine("\t----- Produtos -----"); 
                    produtosVendaCliente.ForEach(y => 
                    {
                        Console.WriteLine($"\t{View.ListarProdutos().SingleOrDefault(z => y.idProduto == z.id).descricao}{y}");
                    });
                }
                Console.WriteLine("------------------------------------------------------------------- \n");
            });
        } else throw new InvalidOperationException("Não há nenhuma Venda cadastrada! \n");
    }

    public static void InformaEntrega()
    {
        List<Venda> vendasEntregar = View.ObterListaVendasEntregar();
        if(vendasEntregar.Any())
        {
            vendasEntregar.ForEach(x => 
            {
                Console.WriteLine($"\t[{x.id}] - {x.dia} - Venda do Cliente: {View.ObterUsuarios(x.idCliente).nome}\n");
                Console.WriteLine("\t----- Produtos -----");
                List<ProdutoVenda> produtosVenda = View.ObterListaProdutosVendas(x.id);

                produtosVenda.ForEach(y => {Console.WriteLine($"\t{View.ListarProdutos().SingleOrDefault(z => y.idProduto == z.id).descricao}{y}");});

                Console.Write("Informe o N° do Cadastro da Venda que foi Entregue: ");
                int id;
                if(int.TryParse(Console.ReadLine(), out id))
                {
                    // Informa Entrega dos Produtos da Venda
                    View.InformaEntregas(id, vendasEntregar);
                } else Console.WriteLine("O valor informado é inválido para essa Operação! \n"); 
            });
        } else Console.WriteLine("Não há nenhum produto a ser entregue!");
    }

}

static class ManterFormasPagamento
{
    public static void MenuFormasPagamento()
    {
        int valor = -1;
        do{
            Console.WriteLine("\n\t---------- Gerenciamento de Formas de Pagamento ---------- \n");
            Console.WriteLine("[1]Realizar Cadastro de Forma de Pagamento  [3]Atualizar Cadastro de Forma de Pagamento");
            Console.WriteLine("[2]Remover Cadastro de Forma de Pagamento   [4]Listar Cadastros de Formas de Pagamento");
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
                            Console.WriteLine("A Operação informada não existe! Informe valores de 0 a 4. \n");
                            break;
                    }
                } else {Console.WriteLine("O valor informado é inválido para essa Operação! \n"); valor = -1;}
            } 
            catch (ArgumentException ex) {Console.WriteLine(ex.Message);}
            catch (InvalidOperationException ex) {Console.WriteLine(ex.Message);}
        } while(valor != 0);
    }

    public static void InserirFormaPagamento()
    {
        Console.WriteLine("\n--- Informações de Cadastro --- \n");
        Console.Write("Informe a Descrição da Forma de Pagamento: ");
        string descricao = Console.ReadLine();
        Console.Write("Informe a Quantidade de Parcelas da Forma de Pagamento: ");
        int parcelas;
        if(int.TryParse(Console.ReadLine(), out parcelas))
        {
            Console.Write("Informe o valor Percentual de Desconto/Juros da Forma de Pagamento (%): ");
            double percentual;
            if(double.TryParse(Console.ReadLine(), out percentual))
            {
                Console.Write("Informe a Quantidade de Dias para o Vencimento na Forma de Pagamento: ");
                int diaVencimento;
                if(int.TryParse(Console.ReadLine(), out diaVencimento))
                {
                    // Operação para Cadastrar a Forma de Pagamento
                    View.InserirFormasPagamento(descricao, parcelas, percentual, diaVencimento);
                } else Console.WriteLine("A Quantidade de Dias para o Vencimento deve ser numérico! \n");
            } else Console.WriteLine("O valor Percentual de Desconto/Juros deve ser numérico! \n"); 
        } else Console.WriteLine("A Quantidade de Parcelas deve ser um valor numérico! \n"); 
    }

    public static void RemoverFormaPagamento()
    {
        ListarFormasPagamento();
        Console.Write("Informe o N° do Cadastro da Forma de Pagamento a ser Removida: ");
        int id;
        if(int.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            View.VerificarIdFormasPagamento(id);

            // Operação para Remover o Cadastro da Forma de Pagamento
            View.RemoverFormasPagamento(id);
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
    }

    public static void AtualizarFormaPagamento()
    {
        ListarFormasPagamento();
        Console.Write("Informe o N° do Cadastro da Forma de Pagamento a ser Atualizada: ");
        int id;
        if(int.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            View.VerificarIdFormasPagamento(id);

            string descricao = "";
            int parcelas = -1;
            double percentual = -1;
            int diaVencimento = -1;
            int alterar = -1;

            do
            {
                Console.WriteLine("\t\t---- O Que Deseja Alterar no Cadastro? ----");
                Console.WriteLine("[1]Descrição [2]Quantidade de Parcelas [3]Percentual de Desconto/Juros [4]Quantidade de Dias para o Vencimento [0]Sair \n");
                Console.Write("Opção: ");
                if(int.TryParse(Console.ReadLine(), out alterar))
                {
                    switch (alterar)
                    {
                        case 0:
                            break;
                        case 1:
                            Console.Write("\nInforme a Nova Descrição da Forma de Pagamento: ");
                            descricao = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("\nInforme a Nova Quantidade de Parcelas da Forma de Pagamento: ");
                            if(!int.TryParse(Console.ReadLine(), out parcelas)) {Console.WriteLine("A Quantidade de Parcelas deve ser um valor númerico! \n"); parcelas = -1;}
                            break;
                        case 3:
                            Console.Write("\nInforme o Novo Percentual de Desconto/Juros da Forma de Pagamento (%): ");
                            if(!double.TryParse(Console.ReadLine(), out percentual)) {Console.WriteLine("O valor Percentual de Desconto/Juros deve ser numérico! \n"); percentual = -1;}
                            break;
                        case 4:
                            Console.Write("\nInforme a Nova Quantidade de Dias para o Vencimento na Forma de Pagamento: ");
                            if(!int.TryParse(Console.ReadLine(), out diaVencimento)) {Console.WriteLine("A Quantidade de Dias para o Vencimento deve ser numérico! \n"); diaVencimento = -1;}
                            break;
                        default:
                            Console.WriteLine("\nA Operação informada não existe! Informe valores de 0 a 4. \n");
                            break;
                    }
                } else {Console.WriteLine("O valor informado é inválido para essa Operação! \n"); alterar = -1;}
            } while (alterar != 0);

            // Operação para Atualizar o Cadastro da Forma de Pagamento
            View.AtualizarFormasPagamento(id, descricao, parcelas, percentual, diaVencimento);
        } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
    }

    public static void ListarFormasPagamento()
    {
        if(View.ListarFormasPagamento().Any())
        {
            Console.WriteLine("\n----- Cadastro de Formas de Pagamento ----- \n");
            View.ListarFormasPagamento().ForEach(x => Console.WriteLine(x));
        } else throw new InvalidOperationException("Não há nenhuma Forma de Pagamento cadastrada! \n");
    }
}

static class Relatorios
{
    public static void MenuRelatorios()
    {
        int valor = -1;
        do{
            Console.WriteLine("\n\t------- Gerenciamento de Relatório de Vendas ------- \n");
            Console.WriteLine("[1]Visualizar Relatório de Venda Parcial [2]Visualizar Relatório de Venda Total");
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
                            RelatorioParcial();
                            break;
                        case 2:
                            RelatorioTotal();
                            break;
                        default:
                            Console.WriteLine("A Operação informada não existe! Informe valores de 0 a 2. \n");
                            break;
                    }
                } else Console.WriteLine("O valor informado é inválido para essa Operação! \n");
            } 
            catch (ArgumentException ex) {Console.WriteLine(ex.Message);}
            catch (InvalidOperationException ex) {Console.WriteLine(ex.Message);}
        } while(valor != 0);
    }

    public static void RelatorioParcial()
    {
        if(View.ListarVendas().Any())
        {
            List<IGrouping<int, Venda>> vendasClientes = View.ObterVendasGeral();

            Console.WriteLine("\t--- Relatório de Vendas por Cliente ---\n");
            foreach(Usuario x in View.ListarUsuarios())
            {
                IGrouping<int, Venda> vendasDoCliente = View.ObterVendasCliente(vendasClientes, x.id);
                if(vendasDoCliente != null)
                {
                    double totalVendasCliente = 0;
                    Console.WriteLine($"[{x.id}] - {x.nome}");

                    foreach(Venda y in vendasDoCliente)
                    {
                        Console.WriteLine($"\t[{y.id}] - {y.dia.AddHours(-3).ToString("dd/MM/yyyy HH:mm", new CultureInfo("pt-BR"))} - Valor da Venda: {y.total.ToString("C", new CultureInfo("pt-BR"))}");

                        List<ProdutoVenda> produtosVendaCliente = View.ObterListaProdutosVendas(y.id);
                        Console.WriteLine("\t----- Produtos -----"); 
                        produtosVendaCliente.ForEach(z => {Console.WriteLine($"\t{View.ListarProdutos().SingleOrDefault(a => z.idProduto == a.id).descricao}{z}");});

                        totalVendasCliente += y.total;
                    }

                    Console.WriteLine($"\n\tValor Total das Vendas do Cliente: {totalVendasCliente.ToString("C", new CultureInfo("pt-BR"))} \n");
                }
            }
        } else throw new InvalidOperationException("Não há nenhuma Venda cadastrada! \n");
    }

    public static void RelatorioTotal()
    {
        if(View.ListarVendas().Any())
        {
            List<IGrouping<int, Venda>> vendasClientes = View.ObterVendasGeral();
            double totalVendasGeral = 0;
            
            Console.WriteLine("\t--- Relatório de Vendas Total ---\n");
            foreach(Usuario x in View.ListarUsuarios())
            {
                IGrouping<int, Venda> vendasDoCliente = View.ObterVendasCliente(vendasClientes, x.id);
                if(vendasDoCliente != null)
                {
                    Console.WriteLine($"[{x.id}] - {x.nome}");

                    foreach(Venda y in vendasDoCliente)
                    {
                        Console.WriteLine($"\t[{y.id}] - {y.dia.AddHours(-3).ToString("dd/MM/yyyy HH:mm", new CultureInfo("pt-BR"))} - Valor da Venda: {y.total.ToString("C", new CultureInfo("pt-BR"))}");

                        List<ProdutoVenda> produtosVendaCliente = View.ObterListaProdutosVendas(y.id);
                        Console.WriteLine("\t----- Produtos -----"); 
                        produtosVendaCliente.ForEach(z => {Console.WriteLine($"\t{View.ListarProdutos().SingleOrDefault(a => z.idProduto == a.id).descricao}{z}");});

                        totalVendasGeral += y.total;
                    }
                }
            }

            Console.WriteLine($"\nValor Total Geral das Vendas: {totalVendasGeral.ToString("C", new CultureInfo("pt-BR"))} \n");
        } else throw new InvalidOperationException("Não há nenhuma Venda cadastrada! \n");
    }
}