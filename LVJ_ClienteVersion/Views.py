from Models.cliente_model import Cliente
from Models.categoria_model import Categoria
from Models.produto_model import Produto
from Models.produto_venda_model import ProdutoVenda
from Models.venda_model import Venda
from Models.persistencia_model import *

class View:
    def __init__(self):
        self.clientes_persistencia = Clientes()  # Criando instância da persistência específica para usuários
        self.cliente = self.clientes_persistencia  # Criando instância do usuário e passando a persistência
        self.categorias_persistencia = Categorias()
        self.categoria = self.categorias_persistencia
        self.produtos_persistencia = Produtos()
        self.produto = self.produtos_persistencia
        self.vendas_persistencia = ProdutoVendas()
        self.venda = self.vendas_persistencia

    def cliente_autenticar(self, email, senha):
        for c in self.cliente.listar():
            if c["email"] == email and c["senha"] == senha:
                return {"id": c["id"], "nome": c["nome"]}  # Mantendo as chaves do JSON
        return None

    def cliente_inserir(self, nome, email, senha, endereco, cep, cpf, admin = False):
        view = View()
        c = Cliente(0, nome, email, senha, endereco, cep, cpf, admin)
        view.cliente.inserir(c)


    def categoria_listar(self):
        return self.categoria.listar()

    def categoria_listar_id(self, id):
        categoria = self.categoria.listar_id(id)
        print(f"Categoria {id} - {categoria["descricao"]}:")
        return categoria
        # return self.categoria.listar_id(id)

    def produto_listar(self):
        return self.produto.listar()


    def produto_inserir(self, descricao, preco, estoque, digital, id_categoria):
        c = Produto(0, descricao, preco, estoque, digital, id_categoria)
        self.produto.inserir(c)
    
    def produto_inserir_venda(self, id_do_usuario, quantidade, preco, digital, codigo, enviado, recebido, id_produto, id_venda):
        preco_total = quantidade * preco
        view = View()

        # 🔹 Criando o objeto corretamente
        c = ProdutoVenda(id_do_usuario, quantidade, preco, preco_total, digital, codigo, enviado, recebido, id_produto, id_venda)

        # 🔹 Chamando a persistência para salvar
        view.venda.inserir_no_carrinho(c)

    def produtovenda_excluir(self, id_do_usuario, quantidade_para_remover, quantidade_atual, preco, valor_total, resgate, codigo, enviado, recebido, id_produto, id_venda):
        novo_preco_total = valor_total - quantidade_para_remover * preco
        nova_quantidade = quantidade_atual - quantidade_para_remover
        if nova_quantidade < 0:
            print("Quantidade a ser removida é maior do que a quantidade atual.")
            return
        # view = View()
        c = ProdutoVenda(id_do_usuario, nova_quantidade, preco, novo_preco_total, resgate, codigo, enviado, recebido, id_produto, id_venda)
        self.venda.atualizar_carrinho(c)

    def carrinho_listar(self):
        return self.venda.listar()

    def id_venda_atual(self):
        return self.venda.carregar_id_venda_atual()

