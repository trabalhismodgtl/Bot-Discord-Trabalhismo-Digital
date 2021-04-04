# Bot Discord Trabalhismo Digital
![image](https://user-images.githubusercontent.com/81885777/113494306-6dfda000-94bd-11eb-882c-ec16ff3c4b18.png)


Bot do discord híbrido com servidor http que gerencia os cargos do servidor [Trabalhismo Digital](https://discord.gg/KZzs4G6HgH)

O gerenciamento depende de um [formulário](https://github.com/PDT-Digital/Formulario-Trabalhismo-Digital).
Insira a url do formulário na segunda linha de um arquivo nomeado SECRETS.txt na mesma pasta que o executável do bot.

O bot cria um localhost:8080 para poder receber sinais do formulário, crie um arquivo SECRETS.js e insira o endereço em que este bot está hospedado, no projeto do [formulário](https://github.com/PDT-Digital/Formulario-Trabalhismo-Digital).

```js
const url = "endereço que está hospedado o bot + port se necessário";

export default url;
```
