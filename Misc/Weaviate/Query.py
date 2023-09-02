import weaviate
import json

client = weaviate.Client(
    url = "https://videomatic-dev-cluster-wirr6unw.weaviate.network",  # Replace with your endpoint
    auth_client_secret=weaviate.AuthApiKey(api_key="BGMlHEev4fm91WMSgFDnX1EXFaBuHUSuHMpl"),  # Replace w/ your Weaviate instance API key
    additional_headers = {
        "X-OpenAI-Api-Key": "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"  # Replace with your inference API key
    }
)

response = (
    client.query
    .get("Question", ["question", "answer", "category"])
    .with_near_text({"concepts": ["biology"]})
    .with_limit(2)
    .do()
)

print(json.dumps(response, indent=4))