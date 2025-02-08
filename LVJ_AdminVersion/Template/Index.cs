UIAdmin.MainUI();

static class UIAdmin
{
    public static void MainUI()
    {
        Usuarios x = new Usuarios();
        Usuario y = new Usuario(0, "q", "q", "q", "q", "q", "q", false);
        x.Inserir(y);
    }
}