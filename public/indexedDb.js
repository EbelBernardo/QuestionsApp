const DATABASE_NAME = "PerguntasDB";
const DATABASE_VERSION = 2;

const STORES = {
    Categories: "Categories",
    Questions: "Questions"
};

let database = null;

async function openDatabase() {

    if (database)
        return database;

    return new Promise((resolve, reject) => {

        const request = indexedDB.open(
            DATABASE_NAME,
            DATABASE_VERSION
        );

        request.onerror = () => reject(request.error);

        request.onsuccess = () => {

            database = request.result;
            resolve(database);

        };

        request.onupgradeneeded = (event) => {

            const db = event.target.result;
            const oldVersion = event.oldVersion;

            // Criação inicial do banco
            if (!db.objectStoreNames.contains(STORES.Categories)) {

                db.createObjectStore(STORES.Categories, {
                    keyPath: "id"
                });

            }

            if (!db.objectStoreNames.contains(STORES.Questions)) {

                db.createObjectStore(STORES.Questions, {
                    keyPath: "id"
                });

            }

            // Atualização da versão 1 para a versão 2
            if (oldVersion < 2) {

                const transaction = event.target.transaction;

                const store = transaction.objectStore(
                    STORES.Questions
                );

                const request = store.getAll();

                request.onsuccess = () => {

                    const questions = request.result;

                    questions.forEach(question => {

                        question.timesAnswered ??= 0;
                        question.correctAnswers ??= 0;
                        question.lastAnswerCorrect ??= false;

                        store.put(question);

                    });

                };

            }

        };

    });

}

window.database = {

    open: async () => {

        await openDatabase();

    },

    // Categories

    createCategory: async (category) => {

        const db = await openDatabase();

        return new Promise((resolve, reject) => {

            const transaction = db.transaction(
                STORES.Categories,
                "readwrite"
            );

            const store = transaction.objectStore(
                STORES.Categories
            );


            const request = store.add(category);


            request.onsuccess = () => {

                resolve(true);

            };


            request.onerror = () => {

                reject(request.error);

            };

        });

    },

    getCategories: async () => {

        const db = await openDatabase();

        return new Promise((resolve, reject) => {

            const transaction = db.transaction(
                STORES.Categories,
                "readonly"
            );

            const store = transaction.objectStore(
                STORES.Categories
            );

            const request = store.getAll();

            request.onsuccess = () => {

                resolve(request.result);

            };

            request.onerror = () => {

                reject(request.error);

            };

        });

    },

    getCategoryById: async (id) => {

        const db = await openDatabase();

        return new Promise((resolve, reject) => {

            const transaction = db.transaction(
                STORES.Categories,
                "readonly"
            );

            const store = transaction.objectStore(
                STORES.Categories
            );

            const request = store.get(id);

            request.onsuccess = () => {

                resolve(request.result);

            };

            request.onerror = () => {

                reject(request.error);

            };

        });

    },

    updateCategory: async (category) => {

        const db = await openDatabase();

        return new Promise((resolve, reject) => {

            const transaction = db.transaction(
                STORES.Categories,
                "readwrite"
            );

            const store = transaction.objectStore(
                STORES.Categories
            );

            const request = store.put(category);

            request.onsuccess = () => {

                resolve(true);

            };

            request.onerror = () => {

                reject(request.error);

            };

        });

    },

    deleteCategory: async (id) => {

        const db = await openDatabase();

        return new Promise((resolve, reject) => {

            const transaction = db.transaction(
                STORES.Categories,
                "readwrite"
            );

            const store = transaction.objectStore(
                STORES.Categories
            );

            const request = store.delete(id);

            request.onsuccess = () => {

                resolve(true);

            };

            request.onerror = () => {

                reject(request.error);

            };

        });

    },

    // Questions

    createQuestion: async (question) => {

        const db = await openDatabase();

        return new Promise((resolve, reject) => {

            const transaction = db.transaction(
                STORES.Questions,
                "readwrite"
            );

            const store = transaction.objectStore(
                STORES.Questions
            );


            const request = store.add(question);


            request.onsuccess = () => {

                resolve(true);

            };


            request.onerror = () => {

                reject(request.error);

            };

        });

    },

    getQuestions: async (categoryId) => {

        const db = await openDatabase();

        return new Promise((resolve, reject) => {

            const transaction = db.transaction(
                STORES.Questions,
                "readonly"
            );

            const store = transaction.objectStore(
                STORES.Questions
            );

            const request = store.getAll();

            request.onsuccess = () => {

                const questions = request.result.filter(
                    question => question.categoryID === categoryId
                );

                resolve(questions);

            };

            request.onerror = () => {

                reject(request.error);

            };

        });

    },

    getQuestionById: async (id) => {

        const db = await openDatabase();

        return new Promise((resolve, reject) => {

            const transaction = db.transaction(
                STORES.Questions,
                "readonly"
            );

            const store = transaction.objectStore(
                STORES.Questions
            );

            const request = store.get(id);

            request.onsuccess = () => {

                resolve(request.result);

            };

            request.onerror = () => {

                reject(request.error);

            };

        });

    },

    updateQuestion: async (question) => {

        const db = await openDatabase();

        return new Promise((resolve, reject) => {

            const transaction = db.transaction(
                STORES.Questions,
                "readwrite"
            );

            const store = transaction.objectStore(
                STORES.Questions
            );

            const request = store.put(question);

            request.onsuccess = () => {

                resolve(true);

            };

            request.onerror = () => {

                reject(request.error);

            };

        });

    },

    deleteQuestion: async (id) => {

        const db = await openDatabase();

        return new Promise((resolve, reject) => {

            const transaction = db.transaction(
                STORES.Questions,
                "readwrite"
            );

            const store = transaction.objectStore(
                STORES.Questions
            );

            const request = store.delete(id);

            request.onsuccess = () => {

                resolve(true);

            };

            request.onerror = () => {

                reject(request.error);

            };

        });

    },

};