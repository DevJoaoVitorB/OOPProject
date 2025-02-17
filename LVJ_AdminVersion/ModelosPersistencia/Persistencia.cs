using System.Text.Json;

class Persistencia<T>
{
    protected List<T> objetos = new List<T>();
    protected string caminho;

    public Persistencia(string caminho) {this.caminho = caminho;}

    public void Inserir(T objeto)
    {
        // Abrir o Arquivo .json com Objetos da Lista de Objetos
        Abrir();
        // Obter o maior ID da Lista de Objetos e somar +1
        int id = objetos.Count == 0 ? 0 : objetos.Max(x => (int)x.GetType().GetProperty("id").GetValue(x));
        var idObjeto = objeto.GetType().GetProperty("id");
        if(idObjeto != null) idObjeto.SetValue(objeto, id + 1);
        // Adicionar o Objeto na Lista de Objetos e Salvar
        objetos.Add(objeto);
        Salvar();
    }

    public void Remover(T objeto)
    {
        // Remover o Objeto da Lista de Objetos e Salvar
        if(objeto != null)
        {
            objetos.Remove(objeto);
            Salvar();
        }
    }

    public void Atualizar(T objeto)
    {
        // Atualizar as Propriedades do Objeto da Lista de Objetos e Salvar
        int id = (int)objeto.GetType().GetProperty("id").GetValue(objeto);
        var x = ListarId(id);

        if(x != null)
        {
            int index = objetos.IndexOf(x);
            objetos[index] = objeto;
            Salvar();
        }
    }

    public List<T> Listar()
    {
        // Retorna a Lista de Objetos
        Abrir();
        return objetos;
    }

    public T ListarId(int id)
    {
        // Retornar o Objeto que possui o ID informado
        Abrir();
        return objetos.SingleOrDefault(x => (int)x.GetType().GetProperty("id").GetValue(x) == id);
    }

    public void Abrir()
    {
        // Abrir ou Criar um Arquivo .json
        objetos.Clear();
        try
        {
            // Abrir o Arquivo .json
            string adicionar = File.ReadAllText(caminho);
            objetos = JsonSerializer.Deserialize<List<T>>(adicionar);
        } catch (FileNotFoundException) {} catch (Exception) {}
    }

    public void Salvar()
    {
        try
        {
            // Salvar os Dados no Arquivo .json
            string salvar = JsonSerializer.Serialize<List<T>>(objetos);
            File.WriteAllText(caminho, salvar);
        } catch (Exception) {}
    } 
}

// Classes que possuem Herança com a Classe Persistencia

class Usuarios : Persistencia<Usuario>
{
    public Usuarios() : base(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_usuarios.json")) {}
}

class Categorias : Persistencia<Categoria>
{
    public Categorias() : base(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_categorias.json")) {}
}

class Produtos : Persistencia<Produto>
{
    public Produtos() : base(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_produtos.json")) {}
}

class ProdutosVendas : Persistencia<ProdutoVenda>
{
    public ProdutosVendas() : base(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_produtos_vendas.json")) {}
}

class Vendas : Persistencia<Venda>
{
    public Vendas() : base(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_vendas.json")) {}
}

class FormasPagamento : Persistencia<FormaPagamento>
{
    public FormasPagamento() : base(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_formas_pagamento.json")) {}
}