async function login() {
    const email = document.getElementById("email").value;
    const senha = document.getElementById("senha").value;
    const erroDisplay = document.getElementById("mensagemErro");

    try {
        const response = await fetch("https://localhost:7281/api/auth/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, senha })
        });

        if (response.ok) {
            const data = await response.json();
            // Salva o token no navegador
            localStorage.setItem("token", data.token);
            // Redireciona para o painel
            window.location.href = "dashboard.html";
        } else {
            erroDisplay.innerText = "Credenciais inválidas!";
        }
    } catch (error) {
        erroDisplay.innerText = "Erro ao conectar com a API. Verifique se ela está rodando!";
    }

}

