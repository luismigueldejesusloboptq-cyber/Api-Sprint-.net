async function fazerLogin() {
    const email = document.getElementById('email').value;
    const senha = document.getElementById('senha').value;
    const erroEl = document.getElementById('mensagemErro');

    // Esconde a mensagem de erro anterior
    erroEl.style.display = 'none';

    try {
        const response = await fetch('/api/auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email: email, senha: senha })
        });

        if (!response.ok) {
            throw new Error('E-mail ou senha incorretos.');
        }

        const data = await response.json();

        // Guarda o Token JWT recebido no armazenamento do navegador
        localStorage.setItem('token_lanchonete', data.token);

        // Redireciona para a tela do painel principal
        window.location.href = 'dashboard.html';

    } catch (error) {
        erroEl.textContent = error.message;
        erroEl.style.display = 'block';
    }
}