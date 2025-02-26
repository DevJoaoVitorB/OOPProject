class FormaPagamento:
    def __init__(self, id, descricao,  parcelas, percentual, diaVencimento):
        self._id = id
        self._descricao = descricao
        self._parcelas = parcelas
        self._percentual = percentual
        self._diaVencimento = diaVencimento

    def to_json(self):
        dic = {}
        dic["id"] = self._id
        dic["descricao"] = self._descricao
        dic["percentual"] = self._percentual
        dic["parcelas"] = self._parcelas
        dic["diaVencimento"] = self._diaVencimento

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
    def percentual(self):
        return self._percentual
    
    @percentual.setter
    def percentual(self, novo_percentual):
        self._percentual = novo_percentual

    @property
    def parcelas(self):
        return self._parcelas
    
    @parcelas.setter
    def parcelas(self, novo_numero_de_parcelas):
        self._parcelas = novo_numero_de_parcelas
    

    @property
    def diaVencimento(self):
        return self._diaVencimento
    
    @diaVencimento.setter
    def diaVencimento(self, novo_diaVencimento):
        self._diaVencimento = novo_diaVencimento

    def __str__(self):
        if self._percentual < 0:
            return f"{self._id} - {self._descricao} \n\t Quantidade de parcelas: {self._parcelas}x \n\t Desconto: {self._percentual * -1}%\n\t Dias para Pagar: {self._diaVencimento} dia(s)\n"
        else:
            return f"{self._id} - {self._descricao} \n\tQuantidade de parcelas: {self._parcelas}x \n\t Juros: {self._percentual}% \n\t Dias para Pagar: {self._diaVencimento} dia(s)\n"
    
    