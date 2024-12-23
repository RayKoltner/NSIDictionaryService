function downloadFile(data, filename) {
    console.log("I've got something over here!");
    const blob = new Blob([new Uint8Array(data)], { type: 'application/xml' });
    const url = window.URL.createObjectURL(blob);

    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();

    document.body.removeChild(a);
    window.URL.revokeObjectURL(url);
}