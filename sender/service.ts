import { ServiceBusSender } from "@azure/service-bus";
import axios from 'axios';
import { Article } from './interfaces';

export async function sendMessage(articles: Article[], sender: ServiceBusSender): Promise<void> {
    try {
        if (articles.length === 0) {
            console.log("Nenhum novo artigo para enviar.");
            return;
        }

        for (const article of articles) {
            await send(article, sender);
        }

        console.log("Todos os artigos foram enviados com sucesso!");

    } catch (error) {
        console.error("Ocorreu um erro ao enviar os artigos:", error);
        throw error;
    }
}

async function send(article: Article, sender: ServiceBusSender) {
    const message = {
        body: article,
        contentType: "application/json",
        label: article.news_site,
        userProperties: {
            title: article.title
        }
    };

    await sender.sendMessages(message);
    console.log(`Artigo "${article.title}" enviado com sucesso!`);
}

export async function getArticlesToSend(): Promise<Article[]> {
    const response = await axios.get(`https://api.spaceflightnewsapi.net/v4/articles/?limit=5`);
    return response.data.results as Article[];
}
