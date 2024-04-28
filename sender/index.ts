import cors from 'cors';
import express from 'express';
import { setupRoutes } from './routes';

const app = express();
const port = 3001;

app.use(cors());
app.use(express.json());

setupRoutes(app);

app.listen(port, () => {
    console.log(`API de envio de mensagens iniciada em http://localhost:${port}`);
});
