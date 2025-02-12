class Produto:

    def __init__(id, descricao, preco, estoque, digital, idCategoria):
        self.id = id
        self.descricao = descricao
        self.preco = preco
        self.estoque = estoque
        self.digital = digital
        self.idCategoria = idCategoria



    #     public int id { get; set; }
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
    def idCategoria(self):
        return self._idCategoria
    @idCategoria.setter
    def idCategoria(self, novo_idCategoria):
        self._idCategoria = novo_idCategoria


    def __str__(self):
        if self._digital == True:
            return f"{self._id} - {self.descricao} \n\tPreço: R${self._preco} \n\tEstoque: {self._estoque} \n\tVersão: Digital \n"
        else:
            return f"{self._id} - {self._descricao} \n\tPreço: R${self._preco} \n\tEstoque: {self._estoque} \n\tVersão: Física \n";
    