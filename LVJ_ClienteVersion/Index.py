from Models.usuario_model import Usuario
from Models.categoria_model import Categoria
from Models.persistencia_model import *
from Models.produto_model import Produto
from Models.produto_venda_model import ProdutoVenda
from Models.venda_model import Venda
from Models.forma_pagamento_model import FormaPagamento

class UIAdmin:
    @staticmethod
    def main_ui():
        x = Usuarios()
        y = Categorias()
        z = Produtos()
        a = ProdutoVendas()
        b = Vendas()
        c = FormaPagamentos()
        x.inserir(Usuario(1, "Gb10", "gb10@email.com", "gb10", "", "", "", True))
        y.inserir(Categoria(1, "Apple", 10))
        z.inserir(Produto(1, "XBOX", 3199.99, 10, False, 1))
        a.inserir(ProdutoVenda(1, 1, 3199.99, 2879.99, False, "", False, False, 1, 1))
        b.inserir(Venda(1, "1000-12-12", True, 13, 2591.99, 1, "2025-02-12", 1, "cartão"))
        c.inserir(FormaPagamento(1, "A Vista", 1, -10, 0))

        x1 = Usuario(1, "Ghost", "ghost000@email.com", "ghost123456789", "", "", "", True)
        y1 = Categoria(1, "Sony", 10)
        z1 = Produto(1, "PlayStation 5", 3199.99, 10, False, 1)
        a1 = ProdutoVenda(1, 1, 3199.99, 2879.99, False, "", False, False, 1, 1)
        b1 = Venda(1, "2025-02-12", True, 13, 2591.99, 1, "2025-02-12", 1, "cartão")
        c1 = FormaPagamento(1, "A Vista", 1, -10, 0)

        x.inserir(x1)
        y.inserir(y1)
        z.inserir(z1)
        a.inserir(a1)
        b.inserir(b1)
        c.inserir(c1)

        # Pegar Informações dos Objetos do Banco de Dados
        print("USUÁRIO")
        for i in x.listar():
            print(i)
        print("CATEGORIA")
        for i in y.listar():
            print(i)
        print("PRODUTO")
        for i in z.listar():
            print(i)
        print("PRODUTO DA VENDA")
        for i in a.listar():
            print(i)
        print("VENDA")
        for i in b.listar():
            print(i)
        print("FORMA DE PAGAMENTO")
        for i in c.listar():
            print(i)

# Chamando a função principal
if __name__ == "__main__":
    UIAdmin.main_ui()
