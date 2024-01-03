var fs = require('fs');
try {
    var path = 'dist/index.html';
    var data = fs.readFileSync(path, 'utf-8');
    var newValue = data.replace(/="\//gim, '="');
    fs.writeFileSync(path, newValue, 'utf-8');
    console.log('Completed post build script successfully');
}
catch (e) {
    console.log('Failed to run post build script successfully.\n' + e.message);
}
