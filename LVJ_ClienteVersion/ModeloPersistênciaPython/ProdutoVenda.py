# class ProdutoVenda
class ProdutoVenda:
    def __init__(self, id, quantidade, precoProduto, precoTotal, resgate, codigo, enviado,recebido, idProduto, idVenda):
        self.id = id
        self.quantidade = quantidade
        self.precoProduto = precoProduto
        self.precoTotal = precoTotal
        self.resgate = resgate
        self.codigo = codigo
        self.enviado = enviado
        self.recebido = recebido
        self.idProduto = idProduto
        self.idVenda = idVenda
    @property
    def id(self):
        return self._id
    
    @id.setter
    def id(self, novo_id):
        self._id = novo_id

    @property
    def quantidade(self):
        return self._quantidade
    
    @quantidade.setter
    def quantidade(self, nova_quantidade):
        self._quantidade = nova_quantidade

    @property
    def preco_produto(self):
        return self._preco_produto
    
    @preco_produto.setter
    def preco_produto(self, novo_preco):
        self._preco_produto = novo_preco

    @property
    def preco_total(self):
        return self._preco_total
    
    @preco_total.setter
    def preco_total(self, novo_preco_total):
        self._preco_total = novo_preco_total

    @property
    def resgate(self):
        return self._resgate
    
    @resgate.setter
    def resgate(self, novo_resgate):
        self._resgate = novo_resgate

    @property
    def codigo(self):
        return self._codigo
    
    @codigo.setter
    def codigo(self, novo_codigo):
        self._codigo = novo_codigo

    @property
    def enviado(self):
        return self._enviado
    
    @enviado.setter
    def enviado(self, novo_enviado):
        self._enviado = novo_enviado

    @property
    def recebido(self):
        return self._recebido
    
    @recebido.setter
    def recebido(self, novo_recebido):
        self._recebido = novo_recebido

    @property
    def id_produto(self):
        return self._id_produto
    
    @id_produto.setter
    def id_produto(self, novo_id_produto):
        self._id_produto = novo_id_produto

    @property
    def id_venda(self):
        return self._id_venda
    
    @id_venda.setter
    def id_venda(self, novo_id_venda):
        self._id_venda = novo_id_venda

    def __str__(self):
        return (f"ID: {self.id}\n"
                f"Quantidade: {self.quantidade}\n"
                f"Preço do Produto: {self.preco_produto}\n"
                f"Preço Total: {self.preco_total}\n"
                f"Resgate: {self.resgate}\n"
                f"Código: {self.codigo}\n"
                f"Enviado: {self.enviado}\n"
                f"Recebido: {self.recebido}\n"
                f"ID do Produto: {self.id_produto}\n"
                f"ID da Venda: {self.id_venda}\n")
