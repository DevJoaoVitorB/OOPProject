from Models.produto_model import Produto
from Models.produto_venda_model import ProdutoVenda
from Models.venda_model import Venda
from Models.persistencia_model import Persistencia

class ManterCarrinho:
    def __init__(self, cliente_id):
        self.cliente_id = cliente_id
        self.carrinho = []  # Lista de produtos no carrinho
        self.persistencia = Persistencia("carrinho.json")
        self.carregar_carrinho()

    def carregar_carrinho(self):
        try:
            with open("carrinho.json", "r") as file:
                dados = json.load(file)
                
                # ⚠️ Correção: garantir que `dados` seja um dicionário
                if not isinstance(dados, dict):
                    dados = {}  

                self.carrinho = dados.get(str(self.cliente_id), [])
        except FileNotFoundError:
            self.carrinho = []
        except json.JSONDecodeError:
            self.carrinho = []

    def salvar_carrinho(self):
        """Salva o carrinho na persistência."""
        dados = self.persistencia.carregar()
        dados[str(self.cliente_id)] = self.carrinho
        self.persistencia.salvar(dados)

    def listar_produtos_do_carrinho(self):
        """Retorna a lista de produtos no carrinho."""
        return self.carrinho

    def inserir_produto_no_carrinho(self, id_produto, quantidade, preco_produto):
        """Adiciona um produto ao carrinho."""
        for item in self.carrinho:
            if item['id_produto'] == id_produto:
                item['quantidade'] += quantidade
                item['preco_total'] = item['quantidade'] * preco_produto
                self.salvar_carrinho()
                return
        novo_produto = {
            "id_produto": id_produto,
            "quantidade": quantidade,
            "preco_produto": preco_produto,
            "preco_total": quantidade * preco_produto
        }
        self.carrinho.append(novo_produto)
        self.salvar_carrinho()

    def atualizar_carrinho(self, id_produto, nova_quantidade):
        """Atualiza a quantidade de um produto no carrinho."""
        for item in self.carrinho:
            if item['id_produto'] == id_produto:
                if nova_quantidade > 0:
                    item['quantidade'] = nova_quantidade
                    item['preco_total'] = nova_quantidade * item['preco_produto']
                else:
                    self.carrinho.remove(item)
                self.salvar_carrinho()
                return

    def remover_produto_do_carrinho(self, id_produto):
        """Remove um produto do carrinho."""
        self.carrinho = [item for item in self.carrinho if item['id_produto'] != id_produto]
        self.salvar_carrinho()

    def limpar_carrinho(self):
        """Esvazia completamente o carrinho do cliente."""
        self.carrinho = []
        self.salvar_carrinho()
