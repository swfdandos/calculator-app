function calculate(operation) {
    var num1 = parseFloat(document.getElementById('input1').value);
    var num2 = parseFloat(document.getElementById('input2').value);
  
    if (isNaN(num1) || isNaN(num2)) {
      document.getElementById('result').innerHTML = "Geçerli sayılar girin.";
      return;
    }
  
    var calculation = {
      operation: operation,
      num1: num1,
      num2: num2
    };
  
    // Mesaj kuyruğuna hesaplama gönderme işlemi burada gerçekleştirilir
    sendToMessageQueue(calculation);
  }
  
  function sendToMessageQueue(calculation) {
    // .NET Core REST API'ye HTTP POST isteği göndererek calculation verisini gönderim
    fetch('https://example.com/api/calculations', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(calculation)
    })
      .then(response => response.json())
      .then(result => processResult(result))
      .catch(error => {
        console.error('Hesaplama gönderimi sırasında bir hata oluştu:', error);
      });
  }
  
  // Csonsumer'dan gelen sonucu işleme alma
  function processResult(result) {
    document.getElementById('result').innerHTML = "Sonuç: " + result;
  }
  
  // Geçmiş verileri MongoDB'den çekme ve gösterme
  function getHistory() {
    // Node.js ile MongoDB'ye bağlanarak geçmiş verileri çekme işlemi burada gerçekleştirilir
    // Geçmiş verileri alın ve arayüzde gösterin
  }
  