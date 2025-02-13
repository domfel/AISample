import { BotSecure } from "./components/BotSecure";
import { Bot } from "./components/Bot";
import { TechLunchBot } from "./components/TechLunchBot";

const AppRoutes = [
    {
        path: '/bot',
        element: <Bot />
    },
    {
        path: '/bot-secure',
        element: <BotSecure />
    },    
    {
        index: true,
        element: <TechLunchBot />
    }
];

export default AppRoutes;
