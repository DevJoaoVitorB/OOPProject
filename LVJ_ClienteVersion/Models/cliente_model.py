class Cliente:
    def __init__(self, id, nome, email, senha, endereco, cep, cpf, admin):
        self._id = id
        self._nome = nome
        self._email = email
        self._senha = senha
        self._endereco = endereco
        self._cep = cep
        self._cpf = cpf
        self._admin = admin
    
    def to_json(self):
        dic = {}
        dic["id"] = self._id
        dic["nome"] = self._nome
        dic["email"] = self._email
        dic["senha"] = self._senha
        dic["endereco"] = self._endereco
        dic["cep"] = self._cep
        dic["cpf"] = self._cpf
        dic["admin"] = self._admin

        return dic
        
            
    
    @property
    def id (self):
        return self._id
    
    @id.setter
    def id(self, novo_id):
        self._id = novo_id

    
    @property
    def nome(self):
        return self._nome
    
    @nome.setter
    def nome(self, novo_nome):
        self._nome = novo_nome

    
    @property
    def email (self):
        return self._email
    
    @email.setter
    def email(self, novo_email):
        self._email = novo_email
   
    @property
    def senha (self):
        return self._senha
    
    @senha.setter
    def senha(self, nova_senha):
        self._senha = nova_senha

    
    @property
    def endereco(self):
        return self._endereco
    
    @endereco.setter
    def endereco(self, novo_endereco):
        self._endereco = novo_endereco
            
    
    @property
    def cep(self):
        return self._cep
    
    @cep.setter
    def cep(self, novo_cep):
        self._cep = novo_cep
            
    
    @property
    def cpf(self):
        return self._cpf
    
    @cpf.setter
    def cpf(self, novo_cpf):
        self._cpf = novo_cpf
            
    
    @property
    def admin(self):
        return self._admin
    
    @admin.setter
    def admin(self, novo_admin):
        self._admin = novo_admin
            
    def __str__(self):
        if (self.admin == True):
            return f"{self.id} - {self.nome} \n\tEmail: {self.email} \n\tSenha: {self.senha} \n"
        else:
              return f"{self.id} - {self.nome} \n\tEmail: {self.email} \n\tSenha: {self.senha} \n\tEndereço: {self.endereco} \n\tCEP: {self.CEP} \n\tCPF: {self.CPF} \n"
