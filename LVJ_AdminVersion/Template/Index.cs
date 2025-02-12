UIAdmin.MainUI();

static class UIAdmin
{
    public static void MainUI()
    {
        // Classe de Persistência
        Usuarios x = new Usuarios();
        Categorias y = new Categorias();
        Produtos z = new Produtos();
        ProdutosVendas a = new ProdutosVendas();
        Vendas b = new Vendas();
        FormasPagamento c = new FormasPagamento();

        // Classe de Modelo
        Usuario x1 = new Usuario(1, "Ghost", "ghost000@email.com", "ghost123456789", "", "", "", true);
        Categoria y1 = new Categoria(1, "Sony", 10);
        Produto z1 = new Produto(1, "PlayStation 5", 3199.99, 10, false, 1);
        ProdutoVenda a1 = new ProdutoVenda(1, 1, 3199.99, 2879.99, false, "", false, false, 1, 1);
        Venda b1 = new Venda(1, DateTime.Today, true, 13, 2591.99, 2591.99, DateTime.Today, 1, 1);
        FormaPagamento c1 = new FormaPagamento(1, "A Vista", 1, -10, 0);

        // Inserir Objetos no Banco de Dados
        x.Inserir(x1);
        y.Inserir(y1);
        z.Inserir(z1);
        a.Inserir(a1);
        b.Inserir(b1);
        c.Inserir(c1);

        // Pegar Informações dos Objetos do Banco de Dados
        Console.WriteLine("USUÁRIO");
        foreach(Usuario i in x.Listar()) Console.WriteLine(i);
        Console.WriteLine("CATEGORIA");
        foreach(Categoria i in y.Listar()) Console.WriteLine(i);
        Console.WriteLine("PRODUTO");
        foreach(Produto i in z.Listar()) Console.WriteLine(i);
        Console.WriteLine("PRODUTO DA VENDA");
        foreach(ProdutoVenda i in a.Listar()) Console.WriteLine(i);
        Console.WriteLine("VENDA");
        foreach(Venda i in b.Listar()) Console.WriteLine(i);
        Console.WriteLine("FORMA DE PAGAMENTO");
        foreach(FormaPagamento i in c.Listar()) Console.WriteLine(i);
    }
}