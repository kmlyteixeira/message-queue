import { ServiceBusClient } from "@azure/service-bus";
import axios from 'axios';

const connectionString = process.env.AZURE_SERVICEBUS_CONNECTION_STRING as string;
const queueName = process.env.AZURE_SERVICEBUS_QUEUE_NAME as string;

const serviceBusClient = new ServiceBusClient(connectionString);
const sender = serviceBusClient.createSender(queueName);

interface Article {
    id: number;
    title: string;
    url: string;
    image_url: string;
    news_site: string;
    summary: string;
    published_at: string;
    updated_at: string;
    featured: boolean;
}

async function sendMessage() {
    try {
        const articles = await getArticlesToSend();

        if (articles.length === 0) {
            console.log("No new articles to send.");
            return;
        }

        for (const article of articles) {
            await send(article);
        }

        console.log("All articles sent successfully!");

    } catch (error) {
        console.log("An error occurred while sending the articles:", error);
    } finally {
        await sender.close();
        await serviceBusClient.close();
    }
}

async function getArticlesToSend(): Promise<Article[]> {
    const response = await axios.get(`https://api.spaceflightnewsapi.net/v4/articles/?limit=5`);
    return response.data.results as Article[];
}

async function send(article: Article) {
    const message = {
        body: JSON.stringify(article),
        contentType: "application/json",
        label: article.news_site,
        userProperties: {
            title: article.title
        }
    };

    await sender.sendMessages(message);
    console.log(`Article "${article.title}" sent successfully!`);
}

sendMessage().catch((error) => {
    console.log("An error occurred:", error);
});
