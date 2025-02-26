class Produto:

    def __init__(self, id, descricao, preco, estoque, digital, idCategoria):
        self.id = id
        self.descricao = descricao
        self.preco = preco
        self.estoque = estoque
        self.digital = digital
        self.id_categoria = idCategoria
    
    def to_json(self):
        dic = {}
        dic["id"] = self._id
        dic["descricao"] = self._descricao
        dic["preco"] = self._preco
        dic["estoque"] = self._estoque
        dic["digital"] = self._digital
        dic["idCategoria"] = self._id_categoria

        return dic    

    @property
    def id(self):
        return self._id
    @id.setter
    def id(self, novo_id):
        self._id = novo_id
    
    
    @property
    def descricao(self):
        return self._descricao
    @descricao.setter
    def descricao(self, nova_descricao):
        self._descricao = nova_descricao
    
    
    @property
    def preco(self):
        return self._rpeco
    @preco.setter
    def preco(self, novo_preco):
        self._preco = novo_preco
    

    @property
    def estoque(self):
        return self._estoque
    @estoque.setter
    def estoque(self, novo_estoque):
        self._estoque = novo_estoque


    @property
    def digital(self):
        return self._id
    @digital.setter
    def digital(self, novo_digital):
        self._digital = novo_digital


    @property
    def id_categoria(self):
        return self.id_categoria
    @id_categoria.setter
    def id_categoria(self, novoid_categoria):
        self.id_categoria = novoid_categoria


    def __str__(self):
        if self._digital == True:
            return f"{self._id} - {self.descricao} \n\tPreço: R${self._preco} \n\tEstoque: {self._estoque} \n\tVersão: Digital \n"
        else:
            return f"{self._id} - {self._descricao} \n\tPreço: R${self._preco} \n\tEstoque: {self._estoque} \n\tVersão: Física \n";
    