// Add this to your wwwroot/js/charts.js file

// Make sure to include Chart.js in your _Host.cshtml or index.html:
// <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

let co2Chart = null;
let environmentalChart = null;

window.renderBarChart = (canvasId, title, labels, data) => {
    const ctx = document.getElementById(canvasId);

    // Destroy existing chart if it exists
    if (co2Chart) {
        co2Chart.destroy();
    }

    co2Chart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: title,
                data: data,
                backgroundColor: [
                    'rgba(75, 192, 192, 0.6)',
                    'rgba(54, 162, 235, 0.6)',
                    'rgba(255, 99, 132, 0.6)'
                ],
                borderColor: [
                    'rgba(75, 192, 192, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 99, 132, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'kg CO2 eq.'
                    }
                }
            },
            plugins: {
                title: {
                    display: true,
                    text: 'CO2 Emissions Analysis',
                    font: {
                        size: 16
                    }
                },
                legend: {
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            return `${context.dataset.label}: ${context.raw} kg CO2 eq.`;
                        }
                    }
                }
            }
        }
    });
};

window.renderRadarChart = (canvasId, labels, data) => {
    const ctx = document.getElementById(canvasId);

    // Destroy existing chart if it exists
    if (environmentalChart) {
        environmentalChart.destroy();
    }

    // Normalize the data to a scale of 0-100 for better visualization
    const maxValue = Math.max(...data.filter(val => !isNaN(val) && val !== null));
    const normalizedData = data.map(val => val !== null && !isNaN(val) ? (val / maxValue) * 100 : 0);

    environmentalChart = new Chart(ctx, {
        type: 'radar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Environmental Impact',
                data: normalizedData,
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 2,
                pointBackgroundColor: 'rgba(255, 99, 132, 1)',
                pointRadius: 4
            }]
        },
        options: {
            responsive: true,
            scales: {
                r: {
                    beginAtZero: true,
                    min: 0,
                    max: 100,
                    ticks: {
                        display: false
                    }
                }
            },
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            const originalValue = data[context.dataIndex];
                            return `${context.label}: ${originalValue}`;
                        }
                    }
                }
            }
        }
    });
};

// Function to create a combined chart showing comparison between materials
window.renderMaterialComparisonChart = (canvasId, materialNames, co2Values) => {
    const ctx = document.getElementById(canvasId);

    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: materialNames,
            datasets: [{
                label: 'CO2 Emissions',
                data: co2Values,
                backgroundColor: materialNames.map((_, i) =>
                    `rgba(${Math.floor(75 + i * 50)}, ${Math.floor(192 - i * 30)}, ${Math.floor(192 - i * 20)}, 0.6)`
                ),
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'kg CO2 eq.'
                    }
                }
            },
            plugins: {
                title: {
                    display: true,
                    text: 'Material CO2 Emissions Comparison',
                    font: {
                        size: 16
                    }
                }
            }
        }
    });
};