using System.Text.Json;

abstract class Persistencia<T>
{
    protected List<T> objetos = new List<T>();

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
        return objetos.FirstOrDefault(x => (int)x.GetType().GetProperty("id").GetValue(x) == id);
    }

    public abstract void Abrir(); 

    public abstract void Salvar(); 
}

// Classes que possuem Herança com a Classe Persistencia

class Usuarios : Persistencia<Usuario>
{
    public override void Abrir()
    {
        // Abrir ou Criar um Arquivo .json
        objetos.Clear();
        try
        {
            // Criar o Caminho para o Banco de Dados!
            string caminho = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_usuarios.json");
            // Abrir o Arquivo .json
            string adicionar = File.ReadAllText(caminho);
            objetos = JsonSerializer.Deserialize<List<Usuario>>(adicionar);
        } catch (FileNotFoundException) {} catch (Exception) {}
    }

    public override void Salvar()
    {
        try
        {
            // Criar o Caminho para o Banco de Dados!
            string caminho = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_usuarios.json");
            // Salvar os Dados no Arquivo .json
            string salvar = JsonSerializer.Serialize<List<Usuario>>(objetos);
            File.WriteAllText(caminho, salvar);
        } catch (Exception) {}
    }
}

class Categorias : Persistencia<Categoria>
{
    public override void Abrir()
    {
        // Abrir ou Criar um Arquivo .json
        objetos.Clear();
        try
        {
            // Criar o Caminho para o Banco de Dados!
            string caminho = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_categorias.json");
            // Abrir o Arquivo .json
            string adicionar = File.ReadAllText(caminho);
            objetos = JsonSerializer.Deserialize<List<Categoria>>(adicionar);
        } catch (FileNotFoundException) {} catch (Exception) {}
    }

    public override void Salvar()
    {
        try
        {
            // Criar o Caminho para o Banco de Dados!
            string caminho = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_categorias.json");
            // Salvar os Dados no Arquivo .json
            string salvar = JsonSerializer.Serialize<List<Categoria>>(objetos);
            File.WriteAllText(caminho, salvar);
        } catch (Exception) {}
    }
}

class Produtos : Persistencia<Produto>
{
    public override void Abrir()
    {
        // Abrir ou Criar um Arquivo .json
        objetos.Clear();
        try
        {
            // Criar o Caminho para o Banco de Dados!
            string caminho = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_produtos.json");
            // Abrir o Arquivo .json
            string adicionar = File.ReadAllText(caminho);
            objetos = JsonSerializer.Deserialize<List<Produto>>(adicionar);
        } catch (FileNotFoundException) {} catch (Exception) {}
    }

    public override void Salvar()
    {
        try
        {
            // Criar o Caminho para o Banco de Dados!
            string caminho = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_produtos.json");
            // Salvar os Dados no Arquivo .json
            string salvar = JsonSerializer.Serialize<List<Produto>>(objetos);
            File.WriteAllText(caminho, salvar);
        } catch (Exception) {}
    }
}

class ProdutosVendas : Persistencia<ProdutoVenda>
{
    public override void Abrir()
    {
        // Abrir ou Criar um Arquivo .json
        objetos.Clear();
        try
        {
            // Criar o Caminho para o Banco de Dados!
            string caminho = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_produtos_vendas.json");
            // Abrir o Arquivo .json
            string adicionar = File.ReadAllText(caminho);
            objetos = JsonSerializer.Deserialize<List<ProdutoVenda>>(adicionar);
        } catch (FileNotFoundException) {} catch (Exception) {}
    }

    public override void Salvar()
    {
        try
        {
            // Criar o Caminho para o Banco de Dados!
            string caminho = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_produtos_vendas.json");
            // Salvar os Dados no Arquivo .json
            string salvar = JsonSerializer.Serialize<List<ProdutoVenda>>(objetos);
            File.WriteAllText(caminho, salvar);
        } catch (Exception) {}
    }
}

class Vendas : Persistencia<Venda>
{
    public override void Abrir()
    {
        // Abrir ou Criar um Arquivo .json
        objetos.Clear();
        try
        {
            // Criar o Caminho para o Banco de Dados!
            string caminho = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_vendas.json");
            // Abrir o Arquivo .json
            string adicionar = File.ReadAllText(caminho);
            objetos = JsonSerializer.Deserialize<List<Venda>>(adicionar);
        } catch (FileNotFoundException) {} catch (Exception) {}
    }

    public override void Salvar()
    {
        try
        {
            // Criar o Caminho para o Banco de Dados!
            string caminho = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_vendas.json");
            // Salvar os Dados no Arquivo .json
            string salvar = JsonSerializer.Serialize<List<Venda>>(objetos);
            File.WriteAllText(caminho, salvar);
        } catch (Exception) {}
    }
}

class FormasPagamento : Persistencia<FormaPagamento>
{
    public override void Abrir()
    {
        // Abrir ou Criar um Arquivo .json
        objetos.Clear();
        try
        {
            // Criar o Caminho para o Banco de Dados!
            string caminho = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_formas_pagamento.json");
            // Abrir o Arquivo .json
            string adicionar = File.ReadAllText(caminho);
            objetos = JsonSerializer.Deserialize<List<FormaPagamento>>(adicionar);
        } catch (FileNotFoundException) {} catch (Exception) {}
    }

    public override void Salvar()
    {
        try
        {
            // Criar o Caminho para o Banco de Dados!
            string caminho = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Banco_Dados", "lista_formas_pagamento.json");
            // Salvar os Dados no Arquivo .json
            string salvar = JsonSerializer.Serialize<List<FormaPagamento>>(objetos);
            File.WriteAllText(caminho, salvar);
        } catch (Exception) {}
    }
}