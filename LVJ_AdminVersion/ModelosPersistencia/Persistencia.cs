using System.Text.Json;

abstract class Persistencia
{
    protected List<object> objetos = new List<object>();

    public void Inserir(object objeto)
    {
        // Abrir o Arquivo .json com Objetos da Lista de Objetos
        Abrir();
        // Obter o maior ID da Lista de Objetos e somar +1
        int id = 0;
        foreach (object x in objetos) if(x.id > id) id = x.id;
        objeto.id = id + 1;
        // Adicionar o Objeto na Lista de Objetos e Salvar
        objetos.Add(objeto);
        Salvar();
    }

    public void Remover(object objeto)
    {
        // Remover o Objeto da Lista de Objetos e Salvar
        if(objeto != null)
        {
            objetos.Remove(objeto);
            Salvar();
        }
    }

    public void Atualizar(object objeto)
    {
        // Atualizar as Propriedades do Objeto da Lista de Objetos e Salvar
        object x = ListarId(objeto.id);
        if(x != null)
        {
            objetos.Remove(x);
            objetos.Add(objeto);
            Salvar();
        }
    }

    public List<object> Listar()
    {
        // Retorna a Lista de Objetos
        Abrir();
        return objetos;
    }

    public object ListarId(int id)
    {
        // Retornar o Objeto que possui o ID informado
        Abrir();
        foreach (object x in objetos) if(x.id == id) return x;
        return null;
    }

    public abstract void Abrir(); 

    public abstract void Salvar(); 
}

// Classes que possuem Herança com a Classe Persistencia

class Usuarios : Persistencia
{
    public override void Abrir()
    {
        // Abrir ou Criar um Arquivo .json
        objetos.Clear();
        try
        {
            string adicionar = File.ReadAllText("lista_usuarios.json");
            objetos = JsonSerializer.Deserialize<List<object>>(adicionar);
        } catch (FileNotFoundException) {}
    }

    public override void Salvar()
    {
        // Salvar os Dados no Arquivo .json
        string salvar = JsonSerializer.Serialize<List<object>>(objetos);
        File.WriteAllText("lista_usuarios.json", salvar);
    }
}

class Categorias : Persistencia
{
    public override void Abrir()
    {
        // Abrir ou Criar um Arquivo .json
        objetos.Clear();
        try
        {
            string adicionar = File.ReadAllText("lista_categorias.json");
            objetos = JsonSerializer.Deserialize<List<object>>(adicionar);
        } catch (FileNotFoundException) {}
    }

    public override void Salvar()
    {
        // Salvar os Dados no Arquivo .json
        string salvar = JsonSerializer.Serialize<List<object>>(objetos);
        File.WriteAllText("lista_categorias.json", salvar);
    }
}

class Produtos : Persistencia
{
    public override void Abrir()
    {
        // Abrir ou Criar um Arquivo .json
        objetos.Clear();
        try
        {
            string adicionar = File.ReadAllText("lista_produtos.json");
            objetos = JsonSerializer.Deserialize<List<object>>(adicionar);
        } catch (FileNotFoundException) {}
    }

    public override void Salvar()
    {
        // Salvar os Dados no Arquivo .json
        string salvar = JsonSerializer.Serialize<List<object>>(objetos);
        File.WriteAllText("lista_produtos.json", salvar);
    }
}

class ProdutosVendas : Persistencia
{
    public override void Abrir()
    {
        // Abrir ou Criar um Arquivo .json
        objetos.Clear();
        try
        {
            string adicionar = File.ReadAllText("lista_produtos_vendas.json");
            objetos = JsonSerializer.Deserialize<List<object>>(adicionar);
        } catch (FileNotFoundException) {}
    }

    public override void Salvar()
    {
        // Salvar os Dados no Arquivo .json
        string salvar = JsonSerializer.Serialize<List<object>>(objetos);
        File.WriteAllText("lista_produtos_vendas.json", salvar);
    }
}

class Vendas : Persistencia
{
    public override void Abrir()
    {
        // Abrir ou Criar um Arquivo .json
        objetos.Clear();
        try
        {
            string adicionar = File.ReadAllText("lista_vendas.json");
            objetos = JsonSerializer.Deserialize<List<object>>(adicionar);
        } catch (FileNotFoundException) {}
    }

    public override void Salvar()
    {
        // Salvar os Dados no Arquivo .json
        string salvar = JsonSerializer.Serialize<List<object>>(objetos);
        File.WriteAllText("lista_vendas.json", salvar);
    }
}

class FormasPagamento : Persistencia
{
    public override void Abrir()
    {
        // Abrir ou Criar um Arquivo .json
        objetos.Clear();
        try
        {
            string adicionar = File.ReadAllText("lista_formas_pagamento.json");
            objetos = JsonSerializer.Deserialize<List<object>>(adicionar);
        } catch (FileNotFoundException) {}
    }

    public override void Salvar()
    {
        // Salvar os Dados no Arquivo .json
        string salvar = JsonSerializer.Serialize<List<object>>(objetos);
        File.WriteAllText("lista_formas_pagamento.json", salvar);
    }
}