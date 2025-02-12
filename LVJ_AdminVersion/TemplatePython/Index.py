class UIAdmin:
    @staticmethod
    def main_ui(self):
        x =  Usuarios()
        y = Usuario(0, "teste", "teste", "teste", "teste", "teste", "teste", False)
        x.inserir(y)
        for z in x.listar():
            print(z)

# Chamada do método principal
UIAdmin.main_ui()