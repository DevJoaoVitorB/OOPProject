UIAdmin.MainUI();

static class UIAdmin
{
    public static void MainUI()
    {
        Usuarios x = new Usuarios();
        Usuario y = new Usuario(0, "teste", "teste", "teste", "teste", "teste", "teste", false);
        x.Inserir(y);
        foreach(Usuario z in x.Listar()) Console.WriteLine(z);
    }
}