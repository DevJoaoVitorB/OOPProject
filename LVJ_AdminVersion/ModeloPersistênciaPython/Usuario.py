class Usuario:
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
    def endereco(self):
        return self._endereco
    
    @endereco.setter
    def endereco(self, nova_endereco):
        self._endereco
            
    
    @property
    def cep(self):
        return self._cep
    
    @cep.setter
    def cep(self, nova_cep):
        self._cep
            
    
    @property
    def cpf(self):
        return self._cpf
    
    @cpf.setter
    def cpf(self, nova_cpf):
        self._cpf
            
    
    @property
    def admin(self):
        return self._admin
    
    @admin.setter
    def admin(self, nova_admin):
        self._admin
            
    def __str__(self):
        if (admin == true):
            return f"{id} - {nome} \n\tEmail: {email} \n\tSenha: {senha} \n"
        else:
              return f"{id} - {nome} \n\tEmail: {email} \n\tSenha: {senha} \n\tEndereço: {endereco} \n\tCEP: {CEP} \n\tCPF: {CPF} \n"
