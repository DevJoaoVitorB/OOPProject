from Models.cliente_model import Cliente
from Models.categoria_model import Categoria
from Models.produto_model import produto
from Models.persistencia_model import *



class View:
    @staticmethod
    def cliente_admin():
        for c in Clientes.listar():
            if c.email == "admin": return None
        View.cliente_inserir("admin", "admin", "0000", "1234")    
    @staticmethod
    def cliente_autenticar(email, senha):
        for c in Clientes.listar():
            if c.email == email and c.senha == senha:
                return { "id" : c.id, "nome" : c.nome }
        return None    

    @staticmethod
    def cliente_listar():
        return Clientes.listar()
    @staticmethod
    def cliente_inserir(nome, email, fone, senha, endereco, cep, cpf, admin):
        c = Cliente(0, nome, email, fone, senha, endereco, cep, cpf, admin)
        Clientes.inserir(c)
    @staticmethod
    def cliente_atualizar(id, nome, email, fone, senha, endereco, cep, cpf, admin):
        c = Cliente(id, nome, email, fone, senha, endereco, cep, cpf, admin)
        Clientes.atualizar(c)
    @staticmethod
    def cliente_excluir(id):
        c = Cliente(id, "", "", "", "","","","")
        Clientes.excluir(c)

    @staticmethod
    def categoria_listar():
        return Categorias.listar()
    @staticmethod
    def categoria_listar_id(id):
        return Categorias.listar_id(id)
    @staticmethod
    def categoria_inserir(descricao, desconto):
        c = Categoria(0, descricao, desconto)
        Categorias.inserir(c)
    @staticmethod
    def categoria_atualizar(id, descricao,desconto):
        c = Categoria(id, descricao, desconto)
        Categorias.atualizar(c)
    @staticmethod
    def categoria_excluir(id):
        c = Categoria(id, "", "")
        Categorias.excluir(c)

    @staticmethod
    def produto_listar():
        return Produtos.listar()
    @staticmethod
    def produto_inserir(descricao, preco, estoque, digital, idCategoria):
        c = Produto(0, descricao, preco, estoque, digital, idCategoria)
        Produtos.inserir(c)
    @staticmethod
    def produto_atualizar(id, descricao, preco, estoque, digital, idCategoria):
        c = Produto(id, descricao, preco, estoque, digital, idCategoria)
        Produtos.atualizar(c)
    @staticmethod
    def produto_excluir(id):
        c = Produto(id, "", 0, 0, 0, 0)
        Produtos.excluir(c)
    @staticmethod
    def produto_reajustar(percentual):
        for obj in View.produto_listar():
            View.produto_atualizar(obj.id, obj.descricao, obj.preco * (1 + percentual), obj.estoque, obj.digital, obj.idCategoria)
        