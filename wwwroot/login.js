async function fazerLogin() {
    const emailInput = document.getElementById("email");
    const senhaInput = document.getElementById("senha");
    const erroDisplay = document.getElementById("mensagemErro");

    const email = emailInput.value.trim();
    const senha = senhaInput.value;

    erroDisplay.style.display = "none";

    if (!email || !senha) {
        erroDisplay.innerText = "Por favor, preencha todos os campos!";
        erroDisplay.style.display = "block";
        return;
    }

    try {
        const response = await fetch("/api/auth/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email: email, senha: senha })
        });

        if (response.ok) {
            const data = await response.json();

            // Salva o token JWT de forma limpa no navegador
            localStorage.setItem("token_jwt", data.token);

            // Redireciona diretamente para a vitrine limpa
            window.location.href = "vitrine.html";
        } else {
            const erroMensagem = await response.text();
            erroDisplay.innerText = erroMensagem || "Credenciais inválidas!";
            erroDisplay.style.display = "block";
        }
    } catch (error) {
        console.error("Erro de conexão:", error);
        erroDisplay.innerText = "Erro ao conectar com a API. Verifique se o servidor está rodando!";
        erroDisplay.style.display = "block";
    }
}