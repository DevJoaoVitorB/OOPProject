class Categoria:
    def __init__(self, id, descricao, desconto):
        self._id = id
        self._descricao = descricao
        self._desconto = desconto


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
    def desconto(self):
        return self._desconto
    @desconto.setter
    def desconto(self, novo_desconto):
        self._desconto = novo_desconto
    
    def __str__(self):
        if self._desconto == 0:
            return f"{self._id} - {self._descricao}\n"
        else:
            return f"{self._id} - {self._descricao} - {self._desconto}% de Desconto \n"
    #     }
    # }
