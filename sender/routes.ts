import { ServiceBusClient } from '@azure/service-bus';
import express from 'express';
import { Article } from './interfaces';
import { getArticlesToSend, sendMessage } from './service';

const connectionString = process.env.AZURE_SERVICEBUS_CONNECTION_STRING as string;
const queueName = process.env.AZURE_SERVICEBUS_QUEUE_NAME as string;

export function setupRoutes(app: express.Application): void {
    app.post('/receive-messages', async (req, res) => {
        const serviceBusClient = new ServiceBusClient(connectionString);
        const sender = serviceBusClient.createSender(queueName);

        try {
            const articles = await getArticles();
            await sendMessage(articles, sender);
            res.status(200).send('Mensagens enviadas com sucesso!');
        } catch (error) {
            console.error("Ocorreu um erro ao enviar as mensagens:", error);
            res.status(500).send('Erro ao enviar as mensagens');
        } finally {
            await sender.close();
            await serviceBusClient.close();
        }
    });

    app.post('/send-message', async (req, res) => {
        const serviceBusClient = new ServiceBusClient(connectionString);
        const sender = serviceBusClient.createSender(queueName);

        console.log("Recebendo mensagem:", req);

        try {
            const article: Article = {
                id: Math.random(),
                title: req.body.title,
                url: req.body.url,
                image_url: req.body.image_url,
                news_site: 'manual',
                summary: req.body.summary,
                published_at: new Date().toISOString(),
                updated_at: new Date().toISOString(),
                featured: false
            };
            console.log("Enviando mensagem:", article);
            await sendMessage([article], sender);
            res.status(200).send('Mensagem enviada com sucesso!');
        } catch (error) {
            console.error("Ocorreu um erro ao enviar a mensagem:", error);
            res.status(500).send('Erro ao enviar a mensagem');
        } finally {
            await sender.close();
            await serviceBusClient.close();
        }
    });
}

async function getArticles(): Promise<Article[]> {
    return getArticlesToSend();
}
