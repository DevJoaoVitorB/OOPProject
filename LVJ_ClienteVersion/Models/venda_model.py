class Venda:
    def __init__(self, id, dia, carrinho, frete, total, parcela, vencimento, id_cliente, id_forma_pagamento):
        self.id = id
        self.dia = dia
        self.carrinho = carrinho
        self.frete = frete
        self.total = total
        self.parcela = parcela
        self.vencimento= vencimento
        self.id_cliente = id_cliente
        self.id_forma_pagamento = id_forma_pagamento

    
    def to_json(self):
        dic = {}
        dic["id"] = self._id
        dic["dia"] = self._dia
        dic["carrinho"] = self._carrinho
        dic["frete"] = self._frete
        dic["total"] = self._total
        dic["parcela"] = self._parcela
        dic["vencimento"] = self._vencimento
        dic["idCliente"] = self._id_cliente
        dic["idFormaPagamento"] = self._id_forma_pagamento

        return dic

        

    @property
    def id(self):
        return self._id
    @id.setter
    def id(self, novo_id):
        self._id = novo_id
    
    @property
    def dia(self):
        return self._dia
    @dia.setter
    def dia(self, novo_dia):
        self._dia = novo_dia
    
    
    @property
    def carrinho(self):
        return self._carrinho
    @carrinho.setter
    def carrinho(self, novo_carrinho):
        self._carrinho = novo_carrinho
    
    
    @property
    def frete(self):
        return self._frete
    @frete.setter
    def frete(self, novo_frete):
        self._frete = novo_frete
    
    
    @property
    def total(self):
        return self._total
    @total.setter
    def total(self, novo_total):
        self._total = novo_total
    
    
    @property
    def parcela(self):
        return self._parcela
    @parcela.setter
    def parcela(self, nova_parcela):
        self._parcela = nova_parcela
    
    
    @property
    def vencimento(self):
        return self._vencimento
    @vencimento.setter
    def vencimento(self, novo_vencimento):
        self._cpf = novo_vencimento
    
    
    
    @property
    def id_cliente(self):
        return self._id_cliente
    @id_cliente.setter
    def id_cliente(self, novo_id_cliente):
        self._id_cliente = novo_id_cliente
    
    @property
    def id_forma_pagamento(self):
        return self._id_forma_pagamento
    @id_forma_pagamento.setter
    def id_forma_pagamento(self, novo_id_forma_pagamento):
        self._id_forma_pagamento = novo_id_forma_pagamento

    def __str__(self):
        if self.id_cliente == True:
             return f"{self.id} - {self.dia} \n\tStatus: Aberta \n\tFrete: R${self.frete:f2} \n\tTotal a Pagar: R${self.total:f2} \n\tParcela: R${self.parcela:f2} \n\tData de Vencimento: {self.vencimento} \n"   
        else:
            return f"{self.id} - {self.dia} \n\tStatus: Fechada \n\tFrete: R${self.frete:f2} \n\tTotal a Pagar: R${self.total:f2} \n\tParcela: R${self.parcela:f2} \n\tData de Vencimento: {self.vencimento} \n"
    
