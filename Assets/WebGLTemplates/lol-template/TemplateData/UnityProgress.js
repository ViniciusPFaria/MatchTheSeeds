function UnityProgress (dom) {
	this.progress = 0.0;
	this.message = "";
	this.dom = dom;

	var parent = dom.parentNode;

	progressDiv = document.createElement("div");
	progressDiv.style.position = "absolute";
	progressDiv.id = 'progress'
	parent.appendChild(progressDiv);
	this.progressDiv = progressDiv;


	this.SetProgress = function (progress) {
		if (this.progress < progress) {
			this.progress = progress;
			this.Update();
		}
	}

	this.SetMessage = function (message) {

	}

	this.Clear = function() {
		this.progressDiv.style.display = "none";
	}

	this.Update = function() {
		this.progressDiv.innerHTML = "Loading Progress: "+Math.round(this.progress*100)+"%";
		if (this.progress > 0 && window.loadingProgress) {
			window.loadingProgress(this.progress);
		}
	}

	this.Update ();
}
