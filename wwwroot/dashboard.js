async function carregarDados() {
    const token = localStorage.getItem("token");

    // Se não tiver token, volta para o login
    if (!token) {
        window.location.href = "login.html";
        return;
    }

    try {
        const response = await fetch("https://localhost:7281/api/pedidos", { // Exemplo com pedidos
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            }
        });

        if (response.status === 401) {
            logout();
            return;
        }

        const dados = await response.json();
        const tabela = document.getElementById("corpoTabela");
        tabela.innerHTML = "";

        dados.forEach(item => {
            tabela.innerHTML += `
                <tr>
                    <td>${item.idPedido || item.id}</td>
                    <td>${item.clienteNome || item.nome}</td>
                    <td>R$ ${item.valorTotal || 0}</td>
                </tr>
            `;
        });
    } catch (error) {
        console.error("Erro ao buscar dados:", error);
    }
}

function logout() {
    localStorage.removeItem("token");
    window.location.href = "login.html";
}

// Executa ao carregar a página
carregarDados();