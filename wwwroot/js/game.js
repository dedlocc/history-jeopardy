$(() => {
    const ws = new WebSocket(`ws://${location.host}/ws`);
    
    ws.onopen = e => {
        console.log('WebSocket opened', e);
    };
    
    ws.onclose = e => {
        console.log('WebSocket closed', e);
    };
    
    ws.onmessage = e => {
        const data = JSON.parse(e.data);
        console.log('WebSocket message received: ', data, e);
        alert(data.result.message);
    };
    
    ws.onerror = e => {
        console.log('WebSocket error', e);
    };
    
    $("#game-grid td:not(.completed)").click(() => {
        ws.send(JSON.stringify({
            jsonrpc: '2.0',
            method: 'hello',
            params: [
                {
                    name: prompt('Do you know da way?')
                }
            ],
            id: null
        }));
    });
});
