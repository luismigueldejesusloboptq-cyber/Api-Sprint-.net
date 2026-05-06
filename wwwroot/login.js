async function fazerLogin() {
    // 1. Captura os elementos da tela
    const emailInput = document.getElementById("email");
    const senhaInput = document.getElementById("senha");
    const erroDisplay = document.getElementById("mensagemErro");

    // 2. Pega os valores digitados limpando espaços em branco
    const email = emailInput.value.trim();
    const senha = senhaInput.value;

    // 3. Limpa mensagens de erro anteriores
    erroDisplay.style.display = "none";
    erroDisplay.innerText = "";

    // 4. Validação básica antes de enviar para o servidor
    if (!email || !senha) {
        erroDisplay.innerText = "Por favor, preencha todos os campos!";
        erroDisplay.style.display = "block";
        return;
    }

    try {
        // 5. Faz a requisição usando rota relativa (evita erro de localhost/porta errada)
        const response = await fetch("/api/auth/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ email: email, senha: senha })
        });

        // 6. Se o servidor responder com sucesso (Status 200-299)
        if (response.ok) {
            const data = await response.json();

            // Salva o token retornado pela API no navegador
            localStorage.setItem("token_jwt", data.token);

            // Redireciona o usuário para o painel principal da lanchonete
            window.location.href = "dashboard.html";
        } else {
            // 7. Se o servidor rejeitar (E-mail ou senha errados)
            const erroMensagem = await response.text();
            erroDisplay.innerText = erroMensagem || "Credenciais inválidas! Verifique e tente novamente.";
            erroDisplay.style.display = "block";
        }
    } catch (error) {
        // 8. Se a API estiver desligada ou houver erro de rede
        console.error("Erro de conexão:", error);
        erroDisplay.innerText = "Erro ao conectar com a API. Verifique se o servidor está rodando!";
        erroDisplay.style.display = "block";
    }
}