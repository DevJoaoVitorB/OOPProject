from ModeloPersistencia.Usuario import Usuario
from ModeloPersistencia.Categoria import Categoria
from ModeloPersistencia.Persistencia import Persistencia
from ModeloPersistencia.Produto import Produto
from ModeloPersistencia.ProdutoVenda import ProdutoVenda
from ModeloPersistencia.Venda import Venda
from ModeloPersistencia.FormaPagamento import FormaPagamento

class UIAdmin:
    @staticmethod
    def MainUI():
        x = Usuario()
        y = Categoria()
        z = Produto()
        a = ProdutoVenda()
        b = Venda()
        c = FormaPagamento()

        x1 = Usuario(1, "Ghost", "ghost000@email.com", "ghost123456789", "", "", "", True)
        y1 = Categoria(1, "Sony", 10)
        z1 = Produto(1, "PlayStation 5", 3199.99, 10, False, 1)
        a1 = ProdutoVenda(1, 1, 3199.99, 2879.99, False, "", False, False, 1, 1)
        b1 = Venda(1, "2025-02-12", True, 13, 2591.99, 2591.99, "2025-02-12", 1, 1)
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
    UIAdmin.MainUI()
